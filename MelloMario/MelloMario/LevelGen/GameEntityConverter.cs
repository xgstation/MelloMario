using MelloMario.BlockObjects;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MelloMario.EnemyObjects;
using MelloMario.Theming;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.LevelGen
{
    class GameEntityConverter : JsonConverter
    {
        private static readonly IEnumerable<Type> AssemblyTypes =
            from type in Assembly.GetExecutingAssembly().GetTypes()
            where typeof(IGameObject).IsAssignableFrom(type)
            select type;

        private readonly GameModel model;
        private readonly IGameWorld world;
        private readonly int grid;
        private readonly GraphicsDevice graphicsDevice;

        private IGameObject objToBePushed;
        private Type type;
        private Point objFullSize;
        private Point objPoint;
        private Point quantity;
        private bool isSingle;
        private string direction;
        private string entrance;
        private string backgroundType;
        private Func<Point, IGameObject> createFunc;
        private Stack<IGameObject> objectStackToBeEncapsulated;
        private ISet<Point> ignoredSet;
        private IList<IGameObject> list;
        private Tuple<bool, string[]> propertyPair;
        private bool isQuestionOrBrick;
        private int length;
        private Listener listener;
        private JToken objToken;

        public GameEntityConverter(GameModel model, GraphicsDevice graphicsDevice, IGameWorld parentGameWorld,
            Listener listener, int gridSize)
        {
            this.graphicsDevice = graphicsDevice;
            this.model = model;
            this.listener = listener;
            world = parentGameWorld;
            grid = gridSize;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(EncapsulatedObject<IGameObject>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            objToken = JToken.Load(reader);
            if (!Util.TryGet(out objPoint, objToken, "Point"))
            {
                Debug.WriteLine("Deserialize fail: No start point provided!");
                return null;
            }
            objPoint = new Point(objPoint.X * grid, objPoint.Y * grid);
            objectStackToBeEncapsulated = new Stack<IGameObject>();

            if (!Util.TryGet(out string typeStr, objToken, "Type"))
            {
                Debug.Print("Deserialize fail: Object token does not contain property \"Type\"");
                return null;
            }

            type = null;
            foreach (Type t in AssemblyTypes)
            {
                type = t.Name == typeStr ? t : null;
                if (type != null)
                {
                    break;
                }
            }
            if (type == null)
            {
                Debug.Print("Deserialize fail: " + typeStr + " not found!");

                return null;
            }
            createFunc = point => (IGameObject) Activator.CreateInstance(type, world, point);
            if (Util.TryGet(out quantity, objToken, "Quantity"))
            {
                ignoredSet = !isSingle && Util.TryReadIgnoreSet(objToken, out ignoredSet) ? ignoredSet : null;
            }
            else
            {
                quantity = new Point(1, 1);
            }
            isSingle = quantity.X == 1 && quantity.Y == 1;
            switch (type.Namespace)
            {
                case "MelloMario.BlockObjects":
                    if (BlockConverter(type, objToken, listener, ref objectStackToBeEncapsulated))
                    {
                        Debug.WriteLine("Deserialize " + type.Name + " success!");
                    }
                    break;
                case "MelloMario.EnemyObjects":
                    if (EnemyConverter(type, objToken, ref objectStackToBeEncapsulated))
                    {
                        Debug.WriteLine("Deserialize " + type.Name + " success!");
                    }
                    break;
                case "MelloMario.ItemObjects":
                    if (ItemConverter(type, objToken, ref objectStackToBeEncapsulated))
                    {
                        Debug.WriteLine("Deserialize " + type.Name + " success!");
                    }
                    break;
                case "MelloMario.MiscObjects":
                    if (BackgroundConverter(type, objToken, ref objectStackToBeEncapsulated))
                    {
                        Debug.WriteLine("Deserialize " + type.Name + " success!");
                    }
                    break;
                default:
                    objectStackToBeEncapsulated.Push(createFunc(objPoint));
                    break;
            }

            return new EncapsulatedObject<IGameObject>(objectStackToBeEncapsulated);
        }

        //TODO: Add serialize method and change CanWrite 
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //TODO: Implement serializer
        }

        #region IGameObject Converters

        private bool ItemConverter(Type type, JToken token, ref Stack<IGameObject> stack)
        {
            if (isSingle)
            {
                stack.Push(createFunc(objPoint));
            }
            else
            {
                Util.BatchCreate(createFunc, objPoint, quantity, new Point(GameConst.GRID, GameConst.GRID), ignoredSet, grid, ref stack);
            }
            return true;
        }

        private bool EnemyConverter(Type type, JToken token, ref Stack<IGameObject> stack)
        {

            if (type.Name == "Piranha")
            {
                float hideTime = Util.TryGet(out hideTime, token, "Property", "HideTime") ? hideTime : 3;
                stack.Push(Activator.CreateInstance(type, world, objPoint, hideTime) as BasePhysicalObject);
            }
            else
            {
                if (type.Name == "Koopa")
                {
                    Koopa.ShellColor shellColor = Util.TryGet(out string color, token, "Property", "Color")
                        ? (color == "Green" ? Koopa.ShellColor.green : Koopa.ShellColor.red)
                        : Koopa.ShellColor.green;
                    createFunc = point => (IGameObject) Activator.CreateInstance(type, world, point, shellColor);
                }
                if (isSingle)
                {
                    stack.Push(createFunc(objPoint));
                }
                else
                {
                    Util.BatchCreate(createFunc, objPoint, quantity, new Point(GameConst.GRID, GameConst.GRID), ignoredSet, grid, ref stack);
                }

            }
            return true;
        }

        private bool BlockConverter(Type type, JToken token, Listener listener, ref Stack<IGameObject> stack)
        {
            isQuestionOrBrick = type.Name == "Brick" || type.Name == "Question";
            if (isQuestionOrBrick && isSingle)
            {
                propertyPair = new Tuple<bool, string[]>(
                    Util.TryGet(out bool isHidden, token, "Property", "IsHidden") && isHidden,
                    Util.TryGet(out string[] itemValues, token, "Property", "ItemValues") ? itemValues : null);
                list = Util.CreateItemList(world, objPoint, listener, propertyPair.Item2);
                objToBePushed = Activator.CreateInstance(type, world, objPoint, propertyPair.Item1) as IGameObject;
                if (list != null && list.Count != 0)
                {
                    GameDatabase.SetEnclosedItem(objToBePushed, list);
                }
                if (type.Name == "Question")
                {
                    (objToBePushed as Question).Initialize();
                }
                if (type.Name == "Brick")
                {
                    (objToBePushed as Brick).Initialize();
                }
                stack.Push(objToBePushed);
            }
            else if (!type.IsAssignableFrom(typeof(Pipeline)))
            {
                if (isSingle)
                {
                    stack.Push(Activator.CreateInstance(type, world, objPoint, false) as BaseGameObject);
                    if (type.Name == "Question")
                    {
                        (objToBePushed as Question).Initialize();
                    }
                    if (type.Name == "Brick")
                    {
                        (objToBePushed as Brick).Initialize();
                    }
                }
                else
                {
                    Util.BatchCreate(
                        point =>
                        {
                            objToBePushed = (IGameObject) Activator.CreateInstance(type, world, point, false);
                            if (type.Name == "Question")
                            {
                                (objToBePushed as Question).Initialize();
                            }
                            if (type.Name == "Brick")
                            {
                                (objToBePushed as Brick).Initialize();
                            }
                            return objToBePushed;
                        },
                        objPoint, quantity, new Point(GameConst.GRID, GameConst.GRID), ignoredSet, grid, ref stack);
                }
            }
            else if (type.Name == "Pipeline")
            {
                if (!Util.TryGet(out length, token, "Property", "Length"))
                {
                    Debug.WriteLine("Deserialize fail: Length of Pipeline is missing.");
                    return false;
                }
                if (!Util.TryGet(out direction, token, "Property", "Direction"))
                {
                    Debug.WriteLine("Deserialize fail: Direction of Pipeline is missing.");
                    return false;
                }
                if (isSingle)
                {
                    list = Util.CreateSinglePipeline(model, world, grid, direction, length, objPoint);
                    foreach (Pipeline pipelineComponent in list)
                    {
                        stack.Push(pipelineComponent);
                    }
                    JToken PiranhaToken;
                    if (Util.TryGet(out PiranhaToken, token, "Property", "Piranha"))
                    {
                        if (
                        Util.TryGet(out string color, PiranhaToken, "Color") &&
                        Util.TryGet(out float hiddenTime, PiranhaToken, "HiddenTime") &&
                        Util.TryGet(out float showTime, PiranhaToken, "ShowTime"))
                        {
                            new Piranha(world, new Point(objPoint.X + 16, objPoint.Y), new Point(32, 32),
                                (int)(hiddenTime * 1000), (int)(showTime * 1000), 32, color);
                        }
                    }
                    if (direction != "NV" && direction != "NH" &&
                        Util.TryGet(out entrance, token, "Property", "Entrance"))
                    {
                        GameDatabase.SetEntranceIndex(list[0] as Pipeline, entrance);
                        GameDatabase.SetEntranceIndex(list[1] as Pipeline, entrance);
                    }
                }
                else
                {
                    objFullSize = direction.Contains("V") ? new Point(GameConst.GRID * 2, GameConst.GRID + GameConst.GRID * length) : new Point(GameConst.GRID + GameConst.GRID * length, GameConst.GRID * 2);
                    Util.BatchCreate(point => Util.CreateSinglePipeline(model, world, grid, direction, length, point), objPoint, quantity, objFullSize,
                        ignoredSet, grid, ref stack);
                }
            }
            return true;
        }

        private bool BackgroundConverter(Type type, JToken token, ref Stack<IGameObject> stack)
        {
            if (Util.TryGet(out backgroundType, token, "Property", "Type"))
            {
                Debug.WriteLine("Deserialize fail: Type of background is not given!");
            }
            ZIndex zIndex = Util.TryGet(out string s, token, "Property", "ZIndex")
                ? (ZIndex) Enum.Parse(typeof(ZIndex), s)
                : ZIndex.background0;
            createFunc = point =>
                (IGameObject) Activator.CreateInstance(type, world, point, backgroundType, zIndex);
            objFullSize = createFunc(new Point()).Boundary.Size;
            if (isSingle)
            {
                stack.Push(createFunc(objPoint));
            }
            else
            {
                Util.BatchCreate(createFunc, objPoint, quantity, objFullSize, ignoredSet, grid, ref stack);
            }
            return true;
        }

        #endregion
    }
}

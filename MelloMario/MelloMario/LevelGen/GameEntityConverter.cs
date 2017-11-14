using MelloMario.BlockObjects;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using MelloMario.EnemyObjects;
using MelloMario.Theming;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Factories;

namespace MelloMario.LevelGen
{
    class PointCompare : IEqualityComparer<Point>
    {
        public bool Equals(Point x, Point y)
        {
            return x == y;
        }

        public int GetHashCode(Point obj)
        {
            return obj.GetHashCode();
        }
    }
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
            createFunc = point => (IGameObject) Activator.CreateInstance(type, world, point, listener);
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
                    Util.TryGet(out string color, token, "Property", "Color");
                    createFunc = point => (IGameObject) Activator.CreateInstance(type, world, point, color);
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

        private Tuple<bool,string[]> GetPropertyPair(JToken token)
        {
            return new Tuple<bool, string[]>(
                Util.TryGet(out bool isHidden, token, "Property", "IsHidden") && isHidden,
                Util.TryGet(out string[] itemValues, token, "Property", "ItemValues") ? itemValues : null);
        }
        private bool BlockConverter(Type type, JToken token, Listener listener, ref Stack<IGameObject> stack)
        {
            isQuestionOrBrick = type.Name == "Brick" || type.Name == "Question";
            if (isQuestionOrBrick)
            {
                if (!isSingle)
                {
                    Dictionary<Point, Tuple<bool, string[]>> dictProperties = new Dictionary<Point, Tuple<bool, string[]>>(new PointCompare());
                    if (Util.TryGet(out JToken propertiesToken, token, "Properties"))
                    {
                        foreach (JToken propertyToken in propertiesToken)
                        {
                            Util.TryGet(out Point index, propertyToken, "Index");
                            Tuple<bool, string[]> newPair = GetPropertyPair(propertyToken);
                            dictProperties.Add(index, newPair);
                        }
                        Util.BatchCreateWithProperties(
                            point =>
                            {
                                objToBePushed = (IGameObject)Activator.CreateInstance(type, world, point, listener, false);
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
                            objPoint,
                            quantity,
                            new Point(32,32), 
                            ignoredSet,
                            grid, ref stack,
                            dictProperties,
                            (obj, pair) =>
                            {
                                IList<IGameObject> newList = Util.CreateItemList(world, obj.Boundary.Location, listener, pair.Item2);
                                if (newList != null && newList.Count != 0)
                                {
                                    GameDatabase.SetEnclosedItem(obj, newList);
                                }
                                if (type.Name == "Question")
                                {
                                    (obj as Question).Initialize();
                                }
                                if (type.Name == "Brick")
                                {
                                    (obj as Brick).Initialize();
                                }
                            });
                    }
                    else
                    {
                        Util.BatchCreate(
                            point =>
                            {
                                objToBePushed = (IGameObject)Activator.CreateInstance(type, world, point, listener, false);
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

                    
                    return true;
                }
                propertyPair = GetPropertyPair(token);
                list = Util.CreateItemList(world, objPoint, listener, propertyPair.Item2);
                objToBePushed = Activator.CreateInstance(type, world, objPoint, listener, propertyPair.Item1) as IGameObject;
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
            else if (type.Name == "Flag")
            {
                foreach (IGameObject obj in GameObjectFactory.Instance.CreateGameObjectGroup("FlagPole", world, objPoint, 7, listener))
                {
                    stack.Push(obj);
                }
            }
            else if (!type.IsAssignableFrom(typeof(Pipeline)))
            {
                if (isSingle)
                {
                    stack.Push(Activator.CreateInstance(type, world, objPoint, listener, false) as BaseGameObject);
                }
                else
                {
                    Util.BatchCreate(
                        point => (IGameObject) Activator.CreateInstance(type, world, point, listener, false),
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
                    list = Util.CreateSinglePipeline(model, world, listener, grid, direction, length, objPoint);
                    foreach (Pipeline pipelineComponent in list)
                    {
                        stack.Push(pipelineComponent);
                    }
                    if (Util.TryGet(out JToken piranhaToken, token, "Property", "Piranha"))
                    {
                        if (
                        Util.TryGet(out string color, piranhaToken, "Color") &&
                        Util.TryGet(out float hiddenTime, piranhaToken, "HiddenTime") &&
                        Util.TryGet(out float showTime, piranhaToken, "ShowTime"))
                        {
                            new Piranha(world, new Point(objPoint.X + 16, objPoint.Y), listener, new Point(32, 48),
                                (int) (hiddenTime * 1000), (int) (showTime * 1000), 32, color);
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
                    Util.BatchCreate(point => Util.CreateSinglePipeline(model, world, listener, grid, direction, length, point), objPoint, quantity, objFullSize,
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

using MelloMario.BlockObjects;
using MelloMario.Factories;
using MelloMario.MiscObjects;
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
        private Point size;
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

        public GameEntityConverter(GameModel model, GraphicsDevice graphicsDevice, IGameWorld parentGameWorld,
            int gridSize)
        {
            this.graphicsDevice = graphicsDevice;
            this.model = model;
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
            var objToken = JToken.Load(reader);

            objectStackToBeEncapsulated = new Stack<IGameObject>();

            if (!Util.TryGet(out string typeStr, objToken, "Type"))
            {
                Debug.Print("Deserialize fail: Object token does not contain property \"Type\"");
                return null;
            }

            type = null;
            foreach (var t in AssemblyTypes)
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

            switch (type.Namespace)
            {
                case "MelloMario.BlockObjects":
                    if (BlockConverter(type, objToken, ref objectStackToBeEncapsulated))
                        Debug.WriteLine("Deserialize " + type.Name + " success!");
                    break;
                case "MelloMario.EnemyObjects":
                    if (EnemyConverter(type, objToken, ref objectStackToBeEncapsulated))
                        Debug.WriteLine("Deserialize " + type.Name + " success!");
                    break;
                case "MelloMario.ItemObjects":
                    if (ItemConverter(type, objToken, ref objectStackToBeEncapsulated))
                        Debug.WriteLine("Deserialize " + type.Name + " success!");
                    break;
                case "MelloMario.MiscObjects":
                    if (BackgroundConverter(type, objToken, ref objectStackToBeEncapsulated))
                        Debug.WriteLine("Deserialize " + type.Name + " success!");
                    break;
                default:
                    if (!Util.TryGet(out Point objPoint, objToken, "Point"))
                    {
                        Debug.WriteLine("Deserialize fail: No start point provided!");
                        return null;
                    }
                    objectStackToBeEncapsulated.Push((IGameObject) Activator.CreateInstance(type, world, objPoint));
                    break;
            }

            return new EncapsulatedObject<IGameObject>(objectStackToBeEncapsulated);
        }

        //TODO: Add serialize method and change CanWrite 
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //TODO: Implement serializer
        }

        private static IList<IGameObject> CreateItemList(IGameWorld world, Point point, params string[] s)
        {
            return s?.Select(t => GameObjectFactory.Instance.CreateGameObject(t, world, point)).ToList();
        }

        private static void BatchCreate<T>(Func<Point, T> func, Point startPoint, Point quantity, Point objSize,
            ICollection<Point> ignoredSet, int grid, ref Stack<IGameObject> stack)
        {
            for (var x = 0; x < quantity.X; x++)
            {
                for (var y = 0; y < quantity.Y; y++)
                {
                    var createLocation = new Point(startPoint.X * grid + x * objSize.X,
                        startPoint.Y * grid + y * objSize.Y);
                    var createIndex = new Point(x + 1, y + 1);
                    if (ignoredSet == null || !ignoredSet.Contains(createIndex))
                    {
                        if (typeof(T).IsAssignableFrom(typeof(IEnumerable<IGameObject>)))
                        {
                            foreach (var obj in (IEnumerable<IGameObject>) func(createLocation))
                            {
                                stack.Push(obj);
                            }
                        }
                        else
                        {
                            stack.Push((IGameObject) func(createLocation));
                        }
                    }
                }
            }
        }

        private static bool TryReadIgnoreSet(JToken token, out ISet<Point> toBeIgnored)
        {
            toBeIgnored = new HashSet<Point>();
            if (token["Ignored"] == null)
            {
                return false;
            }
            var ignoredToken = token["Ignored"].ToList();
            foreach (var t in ignoredToken)
            {
                toBeIgnored.Add(t.ToObject<Point>());
            }
            return true;
        }

        private bool ItemConverter(Type type, JToken token, ref Stack<IGameObject> stack)
        {
            if (!Util.TryGet(out objPoint, token, "Point"))
            {
                Debug.WriteLine("Deserialize fail: No start point provided!");
                return false;
            }
            quantity = Util.TryGet(out quantity, token, "Quantity") ? quantity : new Point(1, 1);
            isSingle = quantity.X == 1 && quantity.Y == 1;
            createFunc = point => (IGameObject) Activator.CreateInstance(type, world, point);
            if (isSingle)
            {
                stack.Push(createFunc(objPoint));
            }
            else
            {
                ignoredSet = TryReadIgnoreSet(token, out var newIgnoredSet) ? newIgnoredSet : null;
                BatchCreate(createFunc, objPoint, quantity, new Point(32, 32), ignoredSet, grid, ref stack);
            }
            return true;
        }

        private bool EnemyConverter(Type type, JToken token, ref Stack<IGameObject> stack)
        {
            if (!Util.TryGet(out objPoint, token, "Point"))
            {
                Debug.WriteLine("Deserialize fail: No start point provided!");
                return false;
            }
            if (type.Name == "Piranha")
            {
                float hideTime = Util.TryGet(out hideTime, token, "Property", "HideTime") ? hideTime : 3;
                stack.Push(Activator.CreateInstance(type, world, objPoint, hideTime) as BasePhysicalObject);
            }
            else
            {
                quantity = Util.TryGet(out quantity, token, "Quantity") ? quantity : new Point(1, 1);
                isSingle = quantity.X == 1 && quantity.Y == 1;
                ignoredSet = !isSingle && TryReadIgnoreSet(token, out ignoredSet) ? ignoredSet : null;
                createFunc = point => (IGameObject) Activator.CreateInstance(type, world, point);
                if (type.Name == "Koopa")
                {
                    Koopa.ShellColor shellColor = Util.TryGet(out string color, token, "Property", "Color")
                        ? (color == "Green" ? Koopa.ShellColor.green : Koopa.ShellColor.red)
                        : (Koopa.ShellColor.green);
                    createFunc = point => (IGameObject) Activator.CreateInstance(type, world, point, shellColor);
                }
                if (isSingle)
                {
                    stack.Push(Activator.CreateInstance(type, world, objPoint) as BasePhysicalObject);
                }
                else
                {
                    BatchCreate(createFunc, objPoint, quantity, new Point(32, 32), ignoredSet, grid, ref stack);
                }

            }
            return true;
        }

        private bool BlockConverter(Type type, JToken token, ref Stack<IGameObject> stack)
        {
            quantity = Util.TryGet(out quantity, token, "Quantity") ? quantity : new Point(1, 1);
            isSingle = quantity.X == 1 && quantity.Y == 1;
            ignoredSet = !isSingle && TryReadIgnoreSet(token, out ignoredSet) ? ignoredSet : null;
            isQuestionOrBrick = type.Name == "Brick" || type.Name == "Question";
            if (!Util.TryGet(out objPoint, token, "Point"))
            {
                Debug.WriteLine("Deserialize fail: No start point provided!");
                return false;
            }
            if (isQuestionOrBrick && isSingle)
            {
                objPoint = new Point(objPoint.X * grid, objPoint.Y * grid);
                propertyPair = new Tuple<bool, string[]>(
                    Util.TryGet(out bool isHidden, token, "Property", "IsHidden") && isHidden,
                    Util.TryGet(out string[] itemValues, token, "Property", "ItemValues") ? itemValues : null);
                list = CreateItemList(world, objPoint, propertyPair.Item2);
                objToBePushed = Activator.CreateInstance(type, world, objPoint, propertyPair.Item1) as IGameObject;
                if (list != null && list.Count != 0)
                    GameDatabase.SetEnclosedItem(objToBePushed, list);
                stack.Push(objToBePushed);
            }
            else if (!type.IsAssignableFrom(typeof(Pipeline)))
            {
                if (isSingle)
                {
                    stack.Push(Activator.CreateInstance(type, world, objPoint, false) as BaseGameObject);
                }
                else
                {
                    ////TODO:optimize it
                    //if (isQuestionOrBrick)
                    //{
                    BatchCreate(point => (IGameObject) Activator.CreateInstance(type, world, point, false), objPoint,
                        quantity, new Point(32, 32), ignoredSet, grid, ref stack);
                    //}
                    //else
                    //{
                    //    BatchCreate(point => (IGameObject)Activator.CreateInstance(type, world, point, true), objPoint, quantity, new Point(32, 32), ignoredSet, ref stack);
                    //    objPoint = new Point(objPoint.X * grid, objPoint.Y * grid);
                    //    var fullSize = new Point(32 * quantity.X, 32 * quantity.Y);
                    //    var safeSize = graphicsDevice.DisplayMode.TitleSafeArea;
                    //    if (fullSize.X <= safeSize.Width && fullSize.Y <= safeSize.Height)
                    //    {
                    //        stack.Push(new CompressedBlock(world, objPoint, fullSize, type));
                    //    }
                    //    else
                    //    {
                    //        var splitNumberX = fullSize.X / safeSize.Width;
                    //        var splitNumberY = fullSize.Y / safeSize.Height;
                    //        var remainX = fullSize.X % safeSize.Width;
                    //        var remainY = fullSize.Y % safeSize.Height;
                    //        for (int i = 0; i < splitNumberX; i++)
                    //        {
                    //            stack.Push(new CompressedBlock(world, new Point(objPoint.X + i * safeSize.Width, objPoint.Y), new Point(safeSize.Width, fullSize.Y), type));
                    //        }
                    //        if (remainX != 0)
                    //        {
                    //            stack.Push(new CompressedBlock(world, new Point(objPoint.X + splitNumberX * safeSize.Width, objPoint.Y), new Point(remainX, fullSize.Y), type));
                    //        }
                    //    }
                    //}
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
                    list = CreateSinglePipeline(direction, length, objPoint);
                    foreach (Pipeline pipelineComponent in list)
                    {
                        stack.Push(pipelineComponent);
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
                    size = direction.Contains("V") ? new Point(64, 32 + 32 * length) : new Point(32 + 32 * length, 64);
                    BatchCreate(point => CreateSinglePipeline(direction, length, point), objPoint, quantity, size,
                        ignoredSet, grid, ref stack);
                }
            }
            return true;
        }

        private bool BackgroundConverter(Type type, JToken token, ref Stack<IGameObject> stack)
        {
            quantity = Util.TryGet(out quantity, token, "Quantity") ? quantity : new Point(1, 1);
            isSingle = quantity.X == 1 && quantity.Y == 1;
            ignoredSet = !isSingle && TryReadIgnoreSet(token, out ignoredSet) ? ignoredSet : null;
            if (Util.TryGet(out backgroundType, token, "Property", "Type"))
            {
                Debug.WriteLine("Deserialize fail: Type of background is not given!");
            }
            var zIndex = Util.TryGet(out string s, token, "Property", "ZIndex")
                ? (ZIndex) Enum.Parse(typeof(ZIndex), s)
                : ZIndex.background0;
            if (!Util.TryGet(out objPoint, token, "Point"))
            {
                Debug.WriteLine("Deserialize fail: No start point provided!");
                return false;
            }
            createFunc = point =>
                (IGameObject) Activator.CreateInstance(type, world, point, backgroundType, zIndex);
            size = createFunc(new Point()).Boundary.Size;
            if (isSingle)
            {
                objPoint = new Point(objPoint.X * grid, objPoint.Y * grid);
                stack.Push(createFunc(objPoint));
            }
            else
            {
                BatchCreate(createFunc, objPoint, quantity, size, ignoredSet, grid, ref stack);
            }
            return true;
        }

        private List<IGameObject> CreateSinglePipeline(string pipelineType, int pipelineLength, Point pipelineLoc)
        {
            var listOfPipelineComponents = new List<IGameObject>();
            pipelineLoc = new Point(pipelineLoc.X * grid, pipelineLoc.Y * grid);
            Pipeline in1 = null;
            Pipeline in2 = null;
            switch (pipelineType)
            {
                case "V":
                    in1 = new Pipeline(world, pipelineLoc, "LeftIn", model);
                    in2 = new Pipeline(world, new Point(pipelineLoc.X + grid, pipelineLoc.Y), "RightIn", model);
                    pipelineLoc = new Point(pipelineLoc.X, pipelineLoc.Y + grid);
                    goto case "NV";
                case "HL":
                    in1 = new Pipeline(world, pipelineLoc, "TopLeftIn");
                    in2 = new Pipeline(world, new Point(pipelineLoc.X, pipelineLoc.Y + grid), "BottomLeftIn");
                    pipelineLoc = new Point(pipelineLoc.X + 32, pipelineLoc.Y);
                    goto case "NH";
                case "HR":
                    in1 = new Pipeline(world, pipelineLoc, "TopRightIn");
                    in2 = new Pipeline(world, new Point(pipelineLoc.X, pipelineLoc.Y + grid), "BottomRightIn");
                    pipelineLoc = new Point(pipelineLoc.X - length * grid, pipelineLoc.Y);
                    goto case "NH";
                case "NV":
                    for (var y = 0; y < pipelineLength; y++)
                    {
                        var loc1 = new Point(pipelineLoc.X, pipelineLoc.Y + grid * y);
                        var loc2 = new Point(pipelineLoc.X + grid, pipelineLoc.Y + grid * y);
                        listOfPipelineComponents.Add(new Pipeline(world, loc1, "Left"));
                        listOfPipelineComponents.Add(new Pipeline(world, loc2, "Right"));
                    }
                    break;
                case "NH":
                    for (var x = 0; x < pipelineLength; x++)
                    {
                        var loc1 = new Point(pipelineLoc.X + grid * x, pipelineLoc.Y);
                        var loc2 = new Point(pipelineLoc.X + grid * x, pipelineLoc.Y + grid);
                        listOfPipelineComponents.Add(new Pipeline(world, loc1, "Top"));
                        listOfPipelineComponents.Add(new Pipeline(world, loc2, "Bottom"));
                    }
                    break;
                default:
                    //DO NOTHING
                    break;
                    ;
            }
            listOfPipelineComponents.Insert(0, in1);
            listOfPipelineComponents.Insert(1, in2);
            return listOfPipelineComponents;
        }
    }
}

#define EXP
//#undef EXP
#define OLD
#undef OLD
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
        private static readonly IEnumerable<Type> assemblyTypes = from type in Assembly.GetExecutingAssembly().GetTypes() where typeof(IGameObject).IsAssignableFrom(type) select type;
        private static readonly IEnumerable<Type> blockTypes = from type in assemblyTypes where type.Namespace == "MelloMario.BlockObjects" select type;
        private static readonly IEnumerable<Type> ItemTypes = from type in assemblyTypes where type.Namespace == "MelloMario.ItemObjects" select type;
        private static readonly IEnumerable<Type> EnemyTypes = from type in assemblyTypes where type.Namespace == "MelloMario.EnemyObjects" select type;
        private static readonly IEnumerable<Type> MiscTypes = from type in assemblyTypes where type.Namespace == "MelloMario.MiscObjects" select type;
        private readonly GameModel model;
        private readonly IGameWorld world;
        private readonly int grid;
        private readonly GraphicsDevice graphicsDevice;
        public GameEntityConverter(GameModel model, GraphicsDevice graphicsDevice, IGameWorld parentGameWorld, int gridSize)
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

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var objToken = JToken.Load(reader);
            var objectStackToBeEncapsulated = new Stack<IGameObject>();

            if (!TryGet(out string typeStr, objToken, "Type"))
            {
                Debug.Print("Deserialize fail: Object token does not contain property \"Type\"");
                return null;
            }

            Type type = null;
            foreach (var t in assemblyTypes)
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
#if EXP

            if (blockTypes.Contains(type))
            {
                if (BlockConverter(type, objToken, ref objectStackToBeEncapsulated))
                    Debug.WriteLine("Deserialize " + type.Name + " success!");
            }
            else if (EnemyTypes.Contains(type) && EnemyConverter(type, objToken, ref objectStackToBeEncapsulated))
            {
                Debug.WriteLine("Deserialize " + type.Name + " success!");
            }
            else if (ItemTypes.Contains(type) && ItemConverter(type, objToken, ref objectStackToBeEncapsulated))
            {
                Debug.WriteLine("Deserialize " + type.Name + " success!");
            }
            else if (MiscTypes.Contains(type))
            {

            }
            else
            {
                if (!TryGet(out Point objPoint, objToken, "Point"))
                {
                    Debug.WriteLine("Deserialize fail: No start point provided!");
                    return null;
                }
                objectStackToBeEncapsulated.Push((IGameObject) Activator.CreateInstance(type, world, objPoint));
            }
#endif

#if OLD
            if (!TryGet(out Point startPoint, objToken, "Point"))
                return null;

            //Read quantity of object
            //If "Quantity" does not exist, default value is 1
            int rows = 1;
            int columns = 1;
            if (TryGet(out Point Quantity, objToken, "Quantity"))
            {
                rows = Quantity.X;
                columns = Quantity.Y;
            }

            //Read object properties if given
            IDictionary<Point, Tuple<bool, string[]>> Properties = null;
            var propertiesJToken = objToken["Properties"];
            if (propertiesJToken != null && propertiesJToken.HasValues)
            {
                Properties = propertiesJToken.ToDictionary(
                  token => TryGet(out Point p, token, "Index") ? p : new Point(),
                  token => new Tuple<bool, string[]>(
                      (TryGet(out bool b, token, "isHidden") ? b : false),
                      (TryGet(out string[] s, token, "ItemValue")) ? s : null));
            }

            //Read ignored entry
            ISet<Point> toBeIgnored = new HashSet<Point>();
            if (objToken["Ignored"] != null)
            {
                var ignoredToken = objToken["Ignored"].ToList();
                foreach (var p in ignoredToken)
                {
                    toBeIgnored.Add(p.ToObject<Point>());
                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (!toBeIgnored.Contains(new Point(i, j)))
                    {
                        Point location = new Point((startPoint.X + i) * grid, (startPoint.Y + j) * grid);
                        Tuple<bool, string[]> property = null;
                        IList<IGameObject> list = new List<IGameObject>();
                        if (Properties == null || !Properties.TryGetValue(new Point(i, j), out property))
                        {
                            property = new Tuple<bool, string[]>(false, null);
                        }
                        else
                        {
                            list = CreateItemList(world, location, property.Item2);
                        }
                        PushNewObject(objectStackToBeEncapsulated, typeStr, location, list, property);
                    }
                }
            }
#endif
            return new EncapsulatedObject<IGameObject>(objectStackToBeEncapsulated);
        }
        //TODO: Add serialize method and change CanWrite 
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //TODO: Implement serializer
        }

        private static bool TryGet<T>(out T obj, JToken token, params string[] p)
        {
            obj = default(T);
            if (token.Type is JTokenType.Array) return false;
            var tempToken = token;
            for (var i = 0; i < p.Length - 1; i++)
            {
                if (tempToken[p[i]] != null)
                {
                    if (tempToken.Type is JTokenType.Array) return false;
                    tempToken = tempToken[p[i]];
                }
                else
                {
                    return false;
                }
            }
            var str = p[p.Length - 1];
            if (tempToken.Type is JTokenType.Array) return false;
            if (tempToken[str] == null) return false;
            obj = tempToken[p[p.Length - 1]].ToObject<T>();
            return true;
        }
        private static IList<IGameObject> CreateItemList(IGameWorld world, Point point, params string[] s)
        {
            return s?.Select(t => GameObjectFactory.Instance.CreateGameObject(t, world, point)).ToList();
        }
#if OLD
        private void PushNewObject(Stack<IGameObject> objectStack, string type, Point location, IList<IGameObject> list, Tuple<bool, string[]> property)
        {
            IGameObject objectToPush;
            switch (type)   
            {
                case "Brick":
                    objectToPush = new Brick(world, location, list, property.Item1);
                    break;
                case "Question":
                    objectToPush = new Question(world, location, list, property.Item1);
                    break;
                case "Pipeline":
                    objectToPush = new Pipeline(world, location, property.Item2[0]);
                    break;
                case "Floor":
                    objectToPush = new Floor(world, location);
                    break;
                case "Stair":
                    objectToPush = new Stair(world, location);
                    break;
                case "ShortCloud":
                    objectToPush = new Background(world, location, type, ZIndex.background3);
                    break;
                case "ShortSmileCloud":
                    objectToPush = new Background(world, location, type, ZIndex.background0);
                    break;
                case "LongCloud":
                    objectToPush = new Background(world, location, type, ZIndex.background1);
                    break;
                case "LongSmileCloud":
                    objectToPush = new Background(world, location, type, ZIndex.background2);
                    break;
                default:
                    objectToPush = GameObjectFactory.Instance.CreateGameObject(type, world, location);
                    break;
            }
            if (objectToPush != null)
            {
                objectStack.Push(objectToPush);
            }
        }

#endif
        private void BatchCreate<T>(Func<Point, T> func, Point startPoint, Point quantity, Point objSize, ICollection<Point> ignoredSet, ref Stack<IGameObject> stack)
        {
            for (var x = 0; x < quantity.X; x++)
            {
                for (var y = 0; y < quantity.Y; y++)
                {
                    var createLocation = new Point(startPoint.X * grid + x * objSize.X, startPoint.Y * grid + y * objSize.Y);
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

        private static bool TryReadIgnoreSet(JToken token, out HashSet<Point> toBeIgnored)
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
            if (!TryGet(out Point objPoint, token, "Point"))
            {
                Debug.WriteLine("Deserialize fail: No start point provided!");
                return false;
            }
            var quantity = TryGet(out Point p, token, "Quantity") ? p : new Point(1, 1);
            var isSingle = quantity.X == 1 && quantity.Y == 1;
            Func<Point, IGameObject> createFunc = point => (IGameObject) Activator.CreateInstance(type, world, point);
            if (isSingle)
            {
                stack.Push(createFunc(objPoint));
            }
            else
            {
                var ignoredSet = TryReadIgnoreSet(token, out var newIgnoredSet) ? newIgnoredSet : null;
                BatchCreate(createFunc, objPoint, quantity, new Point(32, 32), ignoredSet, ref stack);
            }
            return true;
        }
        private bool EnemyConverter(Type type, JToken token, ref Stack<IGameObject> stack)
        {
            if (!TryGet(out Point objPoint, token, "Point"))
            {
                Debug.WriteLine("Deserialize fail: No start point provided!");
                return false;
            }
            if (type.IsAssignableFrom(typeof(Piranha)))
            {
                var hideTime = TryGet(out float f, token, "Property", "HideTime") ? f : 3;
                stack.Push(Activator.CreateInstance(type, world, objPoint, hideTime) as BasePhysicalObject);
            }
            else
            {
                var quantity = TryGet(out Point p, token, "Quantity") ? p : new Point(1, 1);
                var isSingle = quantity.X == 1 && quantity.Y == 1;
                var ignoredSet = !isSingle && TryReadIgnoreSet(token, out var newIgnoredSet) ? newIgnoredSet : null;
                Func<Point, IGameObject> createFunc = point => (IGameObject) Activator.CreateInstance(type, world, point);
                if (type.IsAssignableFrom(typeof(Koopa)))
                {
                    Koopa.ShellColor shellColor = TryGet(out string color, token, "Property", "Color") ? (color == "Green" ? Koopa.ShellColor.green : Koopa.ShellColor.red) : (Koopa.ShellColor.green);
                    createFunc = point => (IGameObject) Activator.CreateInstance(type, world, point, shellColor);
                }
                if (isSingle)
                {
                    stack.Push(Activator.CreateInstance(type, world, objPoint) as BasePhysicalObject);
                }
                else
                {
                    BatchCreate(createFunc, objPoint, quantity, new Point(32, 32), ignoredSet, ref stack);
                }

            }
            return true;
        }
        private bool BlockConverter(Type type, JToken token, ref Stack<IGameObject> stack)
        {
            var quantity = TryGet(out Point p, token, "Quantity") ? p : new Point(1, 1);
            var isSingle = quantity.X == 1 && quantity.Y == 1;
            var ignoredSet = !isSingle && TryReadIgnoreSet(token, out var newIgnoredSet) ? newIgnoredSet : null;
            var isQuestionOrBrick = type.IsAssignableFrom(typeof(Brick)) || type.IsAssignableFrom(typeof(Question));
            if (!TryGet(out Point objPoint, token, "Point"))
            {
                Debug.WriteLine("Deserialize fail: No start point provided!");
                return false;
            }
            if (isQuestionOrBrick && isSingle)
            {
                objPoint = new Point(objPoint.X * grid, objPoint.Y * grid);
                var propertyPair = new Tuple<bool, string[]>(
                    TryGet(out bool isHidden, token, "Property", "IsHidden") && isHidden,
                    TryGet(out string[] itemValues, token, "Property", "ItemValues") ? itemValues : null);
                var list = CreateItemList(world, objPoint, propertyPair.Item2);
                var obj = Activator.CreateInstance(type, world, objPoint, propertyPair.Item1) as BaseGameObject;
                if (list != null && list.Count != 0)
                    GameDataBase.SetEnclosedItem(obj, list);
                stack.Push(obj);
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
                    BatchCreate(point => (IGameObject) Activator.CreateInstance(type, world, point, false), objPoint, quantity, new Point(32, 32), ignoredSet, ref stack);
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
            else if (type.IsAssignableFrom(typeof(Pipeline)))
            {
                if (!TryGet(out int length, token, "Property", "Length"))
                {
                    Debug.WriteLine("Deserialize fail: Length of Pipeline is missing.");
                    return false;
                }
                if (!TryGet(out string dir, token, "Property", "Direction"))
                {
                    Debug.WriteLine("Deserialize fail: Direction of Pipeline is missing.");
                    return false;
                }
                if (isSingle)
                {
                    var pipelineComponents = CreateSinglePipeline(dir, length, objPoint);
                    foreach (Pipeline pipelineComponent in pipelineComponents)
                    {
                        stack.Push(pipelineComponent);
                    }
                    if (dir != "NV" && dir != "NH" && TryGet(out string entrance, token, "Property", "Entrance"))
                    {
                        GameDataBase.SetEntranceIndex(pipelineComponents[0], entrance);
                        GameDataBase.SetEntranceIndex(pipelineComponents[1], entrance);
                    }
                }
                else
                {
                    var pipelineSize = dir.Contains("V") ? new Point(64, 32 + 32 * length) : new Point(32 + 32 * length, 64);
                    BatchCreate(point => CreateSinglePipeline(dir, length, point), objPoint, quantity, pipelineSize, ignoredSet, ref stack);
                }
            }
            return true;
        }

        private List<Pipeline> CreateSinglePipeline(string type, int length, Point objPoint)
        {
            var list = new List<Pipeline>();
            objPoint = new Point(objPoint.X * grid, objPoint.Y * grid);
            Pipeline in1 = null;
            Pipeline in2 = null;
            switch (type)
            {
                case "V":
                    in1 = new Pipeline(world, objPoint, "LeftIn", model);
                    in2 = new Pipeline(world, new Point(objPoint.X + grid, objPoint.Y), "RightIn", model);
                    objPoint = new Point(objPoint.X, objPoint.Y + grid);
                    goto case "NV";
                case "HL":
                    in1 = new Pipeline(world, objPoint, "TopLeftIn");
                    in2 = new Pipeline(world, new Point(objPoint.X, objPoint.Y + grid), "BottomLeftIn");
                    objPoint = new Point(objPoint.X + 32, objPoint.Y);
                    goto case "NH";
                case "HR":
                    in1 = new Pipeline(world, objPoint, "TopRightIn");
                    in2 = new Pipeline(world, new Point(objPoint.X, objPoint.Y + grid), "BottomRightIn");
                    objPoint = new Point(objPoint.X - length * grid, objPoint.Y);
                    goto case "NH";
                case "NV":
                    for (var y = 0; y < length; y++)
                    {
                        var loc1 = new Point(objPoint.X, objPoint.Y + grid * y);
                        var loc2 = new Point(objPoint.X + grid, objPoint.Y + grid * y);
                        list.Add(new Pipeline(world, loc1, "Left"));
                        list.Add(new Pipeline(world, loc2, "Right"));
                    }
                    break;
                case "NH":
                    for (var x = 0; x < length; x++)
                    {
                        var loc1 = new Point(objPoint.X + grid * x, objPoint.Y);
                        var loc2 = new Point(objPoint.X + grid * x, objPoint.Y + grid);
                        list.Add(new Pipeline(world, loc1, "Top"));
                        list.Add(new Pipeline(world, loc2, "Bottom"));
                    }
                    break;
                default:
                    //DO NOTHING
                    break; ;
            }
            list.Insert(0, in1);
            list.Insert(1, in2);
            return list;
        }
    }
}

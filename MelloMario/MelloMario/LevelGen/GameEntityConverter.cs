using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MelloMario.BlockObjects;
using MelloMario.EnemyObjects;
using MelloMario.Factories;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MelloMario.LevelGen
{
    internal class PointCompare : IEqualityComparer<Point>
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

    internal class GameEntityConverter : JsonConverter
    {
        private static readonly IEnumerable<Type> AssemblyTypes =
            from type in Assembly.GetExecutingAssembly().GetTypes()
            where typeof(IGameObject).IsAssignableFrom(type)
            select type;

        private readonly GameModel model;
        private readonly IGameWorld world;
        private string backgroundType;
        private Func<Point, IGameObject> createFunc;
        private string direction;
        private string entrance;
        private ISet<Point> ignoredSet;
        private bool isQuestionOrBrick;
        private int length;
        private IList<IGameObject> list;
        private Stack<IGameObject> objectStackToBeEncapsulated;
        private Point objFullSize;
        private Point objPoint;
        private IGameObject objToBePushed;
        private JToken objToken;
        private Vector2 objVector;

        private ProduceMode produceMode;
        private Tuple<bool, string[]> propertyPair;
        private Point quantity;
        private readonly IListener selflistener;
        private Type selftype;
        private Point triangleSize;

        public GameEntityConverter(GameModel model, IGameWorld parentGameWorld, IListener selflistener)
        {
            this.model = model;
            this.selflistener = selflistener;
            world = parentGameWorld;
        }

        //TODO: Add serialize method and change CanWrite 
        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(EncapsulatedObject<IGameObject>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            objToken = JToken.Load(reader);

            if (!Util.TryGet(out objVector, objToken, "Point"))
            {
                Debug.WriteLine("Deserialize fail: No start point provided!");
                return null;
            }
            objPoint = new Point((int) (objVector.X * GameConst.GRID), (int) (objVector.Y * GameConst.GRID));
            objectStackToBeEncapsulated = new Stack<IGameObject>();

            if (!Util.TryGet(out string typeStr, objToken, "Type"))
            {
                Debug.Print("Deserialize fail: Object token does not contain property \"Type\"");
                return null;
            }

            selftype = null;
            foreach (var t in AssemblyTypes)
            {
                selftype = t.Name == typeStr ? t : null;
                if (selftype != null)
                    break;
            }
            if (selftype == null)
            {
                Debug.Print("Deserialize fail: " + typeStr + " not found!");

                return null;
            }
            createFunc = point => (IGameObject) Activator.CreateInstance(selftype, world, point, selflistener);

            produceMode = ProduceMode.One;
            if (Util.TryGet(out quantity, objToken, "Quantity"))
                if (quantity.X * quantity.Y > 1)
                    produceMode = ProduceMode.Rectangle;
            if (Util.TryGet(out triangleSize, objToken, "Triangle"))
                if (Math.Abs(triangleSize.X) > 1 && Math.Abs(triangleSize.Y) > 1)
                    produceMode = ProduceMode.Triangle;
            ignoredSet = !(produceMode is ProduceMode.One) && Util.TryReadIgnoreSet(objToken, out ignoredSet)
                ? ignoredSet
                : null;

            switch (selftype.Namespace)
            {
                case "MelloMario.BlockObjects":
                    if (BlockConverter(selftype, objToken, selflistener, ref objectStackToBeEncapsulated))
                        Debug.WriteLine("Deserialize " + selftype.Name + " success!");
                    break;
                case "MelloMario.EnemyObjects":
                    if (EnemyConverter(selftype, objToken, ref objectStackToBeEncapsulated))
                        Debug.WriteLine("Deserialize " + selftype.Name + " success!");
                    break;
                case "MelloMario.ItemObjects":
                    if (ItemConverter(selftype, ref objectStackToBeEncapsulated))
                        Debug.WriteLine("Deserialize " + selftype.Name + " success!");
                    break;
                case "MelloMario.MiscObjects":
                    if (BackgroundConverter(selftype, objToken, ref objectStackToBeEncapsulated))
                        Debug.WriteLine("Deserialize " + selftype.Name + " success!");
                    break;
                default:
                    objectStackToBeEncapsulated.Push(createFunc(objPoint));
                    break;
            }

            return new EncapsulatedObject<IGameObject>(objectStackToBeEncapsulated);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //TODO: Implement serializer
        }

        private enum ProduceMode
        {
            One,
            Rectangle,
            Triangle
        }

        #region IGameObject Converters

        private bool ItemConverter(Type type, ref Stack<IGameObject> stack)
        {
            createFunc = point => (IGameObject) Activator.CreateInstance(type, world, point, selflistener, false);
            if (produceMode is ProduceMode.One)
                stack.Push(createFunc(objPoint));
            else if (produceMode is ProduceMode.Rectangle)
                Util.BatchCreate(createFunc, objPoint, quantity, new Point(GameConst.GRID, GameConst.GRID), ignoredSet,
                    ref stack);
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
                    createFunc = point =>
                        (IGameObject) Activator.CreateInstance(type, world, point, selflistener, color);
                }
                if (produceMode is ProduceMode.One)
                    stack.Push(createFunc(objPoint));
                else if (produceMode is ProduceMode.Rectangle)
                    Util.BatchCreate(createFunc, objPoint, quantity, new Point(GameConst.GRID, GameConst.GRID),
                        ignoredSet, ref stack);
            }
            return true;
        }

        private static Tuple<bool, string[]> GetPropertyPair(JToken token)
        {
            return new Tuple<bool, string[]>(Util.TryGet(out bool isHidden, token, "Property", "IsHidden") && isHidden,
                Util.TryGet(out string[] itemValues, token, "Property", "ItemValues") ? itemValues : null);
        }

        private bool BlockConverter(Type type, JToken token, IListener listener, ref Stack<IGameObject> stack)
        {
            isQuestionOrBrick = type.Name == "Brick" || type.Name == "Question";
            if (isQuestionOrBrick)
            {
                if (!(produceMode is ProduceMode.One))
                {
                    var dictProperties = new Dictionary<Point, Tuple<bool, string[]>>(new PointCompare());
                    if (Util.TryGet(out JToken propertiesToken, token, "Properties"))
                    {
                        foreach (var propertyToken in propertiesToken)
                        {
                            Util.TryGet(out Point index, propertyToken, "Index");
                            var newPair = GetPropertyPair(propertyToken);
                            dictProperties.Add(index, newPair);
                        }
                        Util.BatchCreateWithProperties(point =>
                        {
                            objToBePushed = (IGameObject) Activator.CreateInstance(type, world, point, listener, false);
                            if (type.Name == "Question")
                                (objToBePushed as Question).Initialize();
                            if (type.Name == "Brick")
                                (objToBePushed as Brick).Initialize();
                            return objToBePushed;
                        }, objPoint, quantity, new Point(32, 32), ignoredSet, ref stack, dictProperties, (obj, pair) =>
                        {
                            var newList = Util.CreateItemList(world, obj.Boundary.Location, listener, pair.Item2);
                            if (newList != null && newList.Count != 0)
                                GameDatabase.SetEnclosedItem(obj, newList);
                            if (type.Name == "Question")
                                (obj as Question).Initialize();
                            if (type.Name == "Brick")
                                (obj as Brick).Initialize();
                        });
                    }
                    else if (produceMode is ProduceMode.Rectangle)
                        Util.BatchCreate(point =>
                        {
                            objToBePushed = (IGameObject) Activator.CreateInstance(type, world, point, listener, false);
                            if (type.Name == "Question")
                                (objToBePushed as Question).Initialize();
                            if (type.Name == "Brick")
                                (objToBePushed as Brick).Initialize();
                            return objToBePushed;
                        }, objPoint, quantity, new Point(GameConst.GRID, GameConst.GRID), ignoredSet, ref stack);
                    else if (produceMode is ProduceMode.Triangle)
                        Util.TriganleCreate(point =>
                        {
                            objToBePushed = (IGameObject) Activator.CreateInstance(type, world, point, listener, false);
                            if (type.Name == "Question")
                                (objToBePushed as Question).Initialize();
                            if (type.Name == "Brick")
                                (objToBePushed as Brick).Initialize();
                            return objToBePushed;
                        }, objPoint, triangleSize, new Point(GameConst.GRID, GameConst.GRID), ignoredSet, ref stack);
                    return true;
                }
                propertyPair = GetPropertyPair(token);
                list = Util.CreateItemList(world, objPoint, listener, propertyPair.Item2);
                objToBePushed =
                    Activator.CreateInstance(type, world, objPoint, listener, propertyPair.Item1) as IGameObject;
                if (list != null && list.Count != 0)
                    GameDatabase.SetEnclosedItem(objToBePushed, list);
                if (type.Name == "Question")
                    (objToBePushed as Question).Initialize(propertyPair.Item1);
                if (type.Name == "Brick")
                    (objToBePushed as Brick).Initialize(propertyPair.Item1);
                stack.Push(objToBePushed);
            }
            else if (type.Name == "Flag")
            {
                bool hasHeight = Util.TryGet(out int Height, token, "Property", "Height");
                foreach (var obj in GameObjectFactory.Instance.CreateGameObjectGroup("FlagPole", world, objPoint,
                    hasHeight ? Height : 7, listener))
                    stack.Push(obj);
            }
            else if (!type.IsAssignableFrom(typeof(Pipeline)))
                switch (produceMode)
                {
                    case ProduceMode.One:
                        stack.Push(Activator.CreateInstance(type, world, objPoint, listener, false) as BaseGameObject);
                        break;
                    case ProduceMode.Rectangle:
                    {
                        Util.BatchCreate(
                            point => (IGameObject) Activator.CreateInstance(type, world, point, listener, false),
                            objPoint, quantity, new Point(GameConst.GRID, GameConst.GRID), ignoredSet, ref stack);
                        break;
                    }

                    case ProduceMode.Triangle:
                    {
                        Util.TriganleCreate(
                            point => (IGameObject) Activator.CreateInstance(type, world, point, listener, false),
                            objPoint, triangleSize, new Point(GameConst.GRID, GameConst.GRID), ignoredSet, ref stack);
                        break;
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
                bool hasIndex = Util.TryGet(out string pipelineIndex, token, "Property", "Index");
                bool isPortalTo = Util.TryGet(out string portalTo, token, "Property", "PortalTo");
                if (produceMode is ProduceMode.One)
                {
                    list = Util.CreateSinglePipeline(model, world, listener, direction, length, objPoint);
                    foreach (Pipeline pipelineComponent in list)
                        stack.Push(pipelineComponent);
                    if (Util.TryGet(out JToken piranhaToken, token, "Property", "Piranha"))
                        if (Util.TryGet(out string color, piranhaToken, "Color") &&
                            Util.TryGet(out float hiddenTime, piranhaToken, "HiddenTime") &&
                            Util.TryGet(out float showTime, piranhaToken, "ShowTime"))
                            new Piranha(world, new Point(objPoint.X + 16, objPoint.Y), listener, new Point(32, 48),
                                (int) (hiddenTime * 1000), (int) (showTime * 1000), 32, color);
                    if (direction != "NV" && direction != "NH")
                    {
                        if (Util.TryGet(out entrance, token, "Property", "Entrance"))
                        {
                            GameDatabase.SetEntranceIndex(list[0] as Pipeline, entrance);
                            GameDatabase.SetEntranceIndex(list[1] as Pipeline, entrance);
                        }
                        if (hasIndex)
                            GameDatabase.AddPipelineIndex(pipelineIndex,
                                new Tuple<Pipeline, Pipeline>(list[0] as Pipeline, list[1] as Pipeline));
                        if (isPortalTo)
                        {
                            GameDatabase.AddPortal(list[0] as Pipeline, portalTo);
                            GameDatabase.AddPortal(list[1] as Pipeline, portalTo);
                        }
                    }
                }
                else
                {
                    objFullSize = direction.Contains("V")
                        ? new Point(GameConst.GRID * 2, GameConst.GRID + GameConst.GRID * length)
                        : new Point(GameConst.GRID + GameConst.GRID * length, GameConst.GRID * 2);
                    Util.BatchCreate(
                        point => Util.CreateSinglePipeline(model, world, listener, direction, length, point), objPoint,
                        quantity, objFullSize, ignoredSet, ref stack);
                }
            }
            return true;
        }

        private bool BackgroundConverter(Type type, JToken token, ref Stack<IGameObject> stack)
        {
            if (Util.TryGet(out backgroundType, token, "Property", "Type"))
                Debug.WriteLine("Deserialize fail: Type of background is not given!");
            var zIndex = Util.TryGet(out string s, token, "Property", "ZIndex")
                ? (ZIndex) Enum.Parse(typeof(ZIndex), s)
                : ZIndex.Background0;
            createFunc = point => (IGameObject) Activator.CreateInstance(type, world, point, backgroundType, zIndex);
            objFullSize = createFunc(new Point()).Boundary.Size;
            if (produceMode is ProduceMode.One)
                stack.Push(createFunc(objPoint));
            else
                Util.BatchCreate(createFunc, objPoint, quantity, objFullSize, ignoredSet, ref stack);
            return true;
        }

        #endregion
    }
}
namespace MelloMario.LevelGen.JsonConverters
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using MelloMario.Factories;
    using MelloMario.Objects;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Enemies;
    using MelloMario.Objects.Items;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    #endregion

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

        private readonly IModel model;
        private readonly IListener<IGameObject> listener;
        private readonly IListener<ISoundable> soundListener;
        private readonly IWorld world;
        private string backgroundType;
        private Func<Point, IGameObject> createFunc;
        private string direction;
        private string entrance;
        private ISet<Point> ignoredSet;
        private int length;
        private IList<IGameObject> list;
        private Stack<IGameObject> objectStackToBeEncapsulated;
        private Point objFullSize;
        private Point objPoint;
        private JToken token;
        private Vector2 objVector;

        private ProduceMode produceMode;
        private Tuple<bool, string[]> propertyPair;
        private Point quantity;
        private Type type;
        private Point triangleSize;

        public GameEntityConverter(
            IModel model,
            IWorld parentWorld,
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener)
        {
            this.model = model;
            this.listener = listener;
            this.soundListener = soundListener;
            world = parentWorld;
        }

        //TODO: Add serialize method and change CanWrite 
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(EncapsulatedObject<IGameObject>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            token = JToken.Load(reader);

            if (!Util.TryGet(out objVector, token, "Point"))
            {
                Debug.WriteLine("Deserialize fail: No start point provided!");
                return null;
            }
            objPoint = new Point((int) (objVector.X * Const.GRID), (int) (objVector.Y * Const.GRID));
            objectStackToBeEncapsulated = new Stack<IGameObject>();

            if (!Util.TryGet(out string typeStr, token, "Type"))
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
            createFunc = point => (type.Name == "Brick" || type.Name == "Question" ?
            Activator.CreateInstance(type, world, point, listener, soundListener, false) :
            Activator.CreateInstance(type, world, point, listener))
            as IGameObject;

            produceMode = ProduceMode.One;
            if (Util.TryGet(out quantity, token, "Quantity"))
            {
                if (quantity.X * quantity.Y > 1)
                {
                    produceMode = ProduceMode.Rectangle;
                }
            }
            if (Util.TryGet(out triangleSize, token, "Triangle"))
            {
                if (Math.Abs(triangleSize.X) > 1 && Math.Abs(triangleSize.Y) > 1)
                {
                    produceMode = ProduceMode.Triangle;
                }
            }
            ignoredSet = !(produceMode is ProduceMode.One) && Util.TryReadIgnoreSet(token, out ignoredSet)
                ? ignoredSet
                : null;

            switch (type.Namespace)
            {
                case "MelloMario.Objects.Blocks":
                    if (BlockConverter())
                    {
                        Debug.WriteLine("Deserialize " + type.Name + " success!");
                    }
                    break;
                case "MelloMario.Objects.Enemies":
                    if (EnemyConverter())
                    {
                        Debug.WriteLine("Deserialize " + type.Name + " success!");
                    }
                    break;
                case "MelloMario.Objects.Items":
                    if (ItemConverter())
                    {
                        Debug.WriteLine("Deserialize " + type.Name + " success!");
                    }
                    break;
                case "MelloMario.Objects.Miscs":
                    if (BackgroundConverter())
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

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //TODO: Implement serializer
        }

        public enum ProduceMode
        {
            One,
            Rectangle,
            Triangle
        }

        #region IGameObject Converters

        #region Item Deserializer

        private bool ItemConverter()
        {
            createFunc = point => (IGameObject) Activator.CreateInstance(
                type,
                world,
                point,
                Point.Zero,
                listener,
                false);
            if (produceMode is ProduceMode.One)
            {
                createFunc = point => (IGameObject) Activator.CreateInstance(type, world, point, listener, soundListener, false);
            }
            switch (produceMode)
            {
                case ProduceMode.One:
                    createFunc(objPoint);
                    break;
                case ProduceMode.Rectangle:
                    Util.BatchRecCreate(createFunc, objPoint, quantity, new Point(Const.GRID, Const.GRID), ignoredSet);
                    break;
                case ProduceMode.Triangle:
                    Util.BatchTriCreate(createFunc, objPoint, triangleSize, new Point(Const.GRID, Const.GRID), ignoredSet);
                    break;
                default:
                    goto case ProduceMode.One;
            }
            return true;
        }

        #endregion

        #region Enemy Deserializer

        private bool EnemyConverter()
        {
            if (type.Name == "Piranha")
            {
                float hideTime = Util.TryGet(out hideTime, token, "Property", "HideTime") ? hideTime : 3;
                Activator.CreateInstance(type, world, objPoint, hideTime);
            }
            else
            {
                if (type.Name == "Koopa")
                {
                    Util.TryGet(out string color, token, "Property", "Color");
                    createFunc = point => (IGameObject) Activator.CreateInstance(
                        type,
                        world,
                        point,
                        listener,
                        color);
                }
                if (produceMode is ProduceMode.One)
                {
                    createFunc(objPoint);
                }
                else if (produceMode is ProduceMode.Rectangle)
                {
                    Util.BatchRecCreate(
                        createFunc,
                        objPoint,
                        quantity,
                        new Point(Const.GRID, Const.GRID),
                        ignoredSet);
                }
            }
            return true;
        }
        #endregion

        #region Block Deserializer

        private void CreateBrickOrQuestion()
        {
            createFunc = point =>
            {
                IGameObject gameObj = Activator.CreateInstance(type, world, point, listener, soundListener, false) as IGameObject;
                (gameObj as Question)?.Initialize(propertyPair.Item1);
                (gameObj as Brick)?.Initialize(propertyPair.Item1);
                return gameObj;
            };
            switch (produceMode)
            {
                case ProduceMode.One:
                    propertyPair = Util.GetPropertyPair(token);
                    list = Util.CreateItemList(world, objPoint, listener, soundListener, propertyPair.Item2);
                    IGameObject gameObj = createFunc(objPoint);
                    if (list != null && list.Count != 0)
                    {
                        Database.SetEnclosedItem(gameObj, list);
                    }
                    (gameObj as Question)?.Initialize(propertyPair.Item1);
                    (gameObj as Brick)?.Initialize(propertyPair.Item1);
                    break;
                default:
                    #region GetProperties
                    //TOOD:Finish it
                    Dictionary<Point, Tuple<bool, string[]>> propertiesDict = new Dictionary<Point, Tuple<bool, string[]>>(new PointCompare());
                    if (Util.TryGet(out JToken propertiesToken, token, "Properties"))
                    {
                        foreach (JToken propertyToken in propertiesToken)
                        {
                            Util.TryGet(out Point index, propertyToken, "Index");
                            Tuple<bool, string[]> newPair = Util.GetPropertyPair(propertyToken);
                            propertiesDict.Add(index, newPair);
                        }
                    }
                    #endregion
                    break;
                case ProduceMode.Rectangle:
                    Util.BatchRecCreate(createFunc, objPoint, quantity, new Point(Const.GRID, Const.GRID), ignoredSet);
                    break;
                case ProduceMode.Triangle:
                    Util.BatchTriCreate(createFunc, objPoint, triangleSize, new Point(Const.GRID, Const.GRID), ignoredSet);
                    break;
            }
        }

        private void CreateSimpleBlock()
        {
            switch (produceMode)
            {
                case ProduceMode.One:
                    Activator.CreateInstance(type, world, objPoint, listener, false);
                    break;
                case ProduceMode.Rectangle:
                    {
                        Util.BatchRecCreate(
                            point => (IGameObject) Activator.CreateInstance(type, world, point, listener, false),
                            objPoint,
                            quantity,
                            new Point(Const.GRID, Const.GRID),
                            ignoredSet);
                        break;
                    }

                case ProduceMode.Triangle:
                    {
                        Util.BatchTriCreate(
                            point => (IGameObject) Activator.CreateInstance(type, world, point, listener, false),
                            objPoint,
                            triangleSize,
                            new Point(Const.GRID, Const.GRID),
                            ignoredSet);
                        break;
                    }
            }
        }

        private void CreatePipeline()
        {
            if (!Util.TryGet(out length, token, "Property", "Length"))
            {
                Debug.WriteLine("Deserialize fail: Length of Pipeline is missing.");
                return;
            }
            if (!Util.TryGet(out direction, token, "Property", "Direction"))
            {
                Debug.WriteLine("Deserialize fail: Direction of Pipeline is missing.");
                return;
            }
            bool hasIndex = Util.TryGet(out string pipelineIndex, token, "Property", "Index");
            bool isPortalTo = Util.TryGet(out string portalTo, token, "Property", "PortalTo");
            if (produceMode is ProduceMode.One)
            {
                list = Util.CreateSinglePipeline(model, world, listener, soundListener, direction, length, objPoint);
                if (Util.TryGet(out JToken piranhaToken, token, "Property", "Piranha"))
                {
                    if (Util.TryGet(out string color, piranhaToken, "Color")
                        && Util.TryGet(out float hiddenTime, piranhaToken, "HiddenTime")
                        && Util.TryGet(out float showTime, piranhaToken, "ShowTime"))
                    {
                        new Piranha(
                            world,
                            new Point(objPoint.X + 16, objPoint.Y),
                            listener,
                            new Point(32, 48),
                            (int) (hiddenTime * 1000),
                            (int) (showTime * 1000),
                            32,
                            color);
                    }
                }
                if (direction != "NV" && direction != "NH")
                {
                    if (Util.TryGet(out entrance, token, "Property", "Entrance"))
                    {
                        Database.SetEntranceIndex(list[0] as Pipeline, entrance);
                        Database.SetEntranceIndex(list[1] as Pipeline, entrance);
                    }
                    if (hasIndex)
                    {
                        Database.AddPipelineIndex(
                            pipelineIndex,
                            new Tuple<Pipeline, Pipeline>(list[0] as Pipeline, list[1] as Pipeline));
                    }
                    if (isPortalTo)
                    {
                        Database.AddPortal(list[0] as Pipeline, portalTo);
                        Database.AddPortal(list[1] as Pipeline, portalTo);
                    }
                }
            }
            else
            {
                objFullSize = direction.Contains("V")
                    ? new Point(Const.GRID * 2, Const.GRID + Const.GRID * length)
                    : new Point(Const.GRID + Const.GRID * length, Const.GRID * 2);
                Util.BatchRecCreate(
                    point => Util.CreateSinglePipeline(model, world, listener, soundListener, direction, length, point),
                    objPoint,
                    quantity,
                    objFullSize,
                    ignoredSet);
            }
        }

        private void CreateFlag()
        {
            bool hasHeight = Util.TryGet(out int Height, token, "Property", "Height");
            GameObjectFactory.Instance.CreateGameObjectGroup(
                "FlagPole",
                world,
                objPoint,
                hasHeight ? Height : 7,
                listener);
        }

        private bool BlockConverter()
        {
            switch (type)
            {
                case Type t when t.Name == "Brick" || t.Name == "Question":
                    CreateBrickOrQuestion();
                    return true;
                case Type t when t.Name == "Flag":
                    CreateFlag();
                    break;
                case Type t when !t.IsAssignableFrom(typeof(Pipeline)):
                    CreateSimpleBlock();
                    break;
                case Type t when t.Name == "Pipeline":
                    CreatePipeline();
                    break;
            }
            return true;
        }

        #endregion

        #region Background Deserializer

        private bool BackgroundConverter()
        {
            if (Util.TryGet(out backgroundType, token, "Property", "Type"))
            {
                Debug.WriteLine("Deserialize fail: Type of background is not given!");
            }
            ZIndex zIndex = Util.TryGet(out string s, token, "Property", "ZIndex")
                ? (ZIndex) Enum.Parse(typeof(ZIndex), s)
                : ZIndex.Background0;
            createFunc = point => (IGameObject) Activator.CreateInstance(type, world, point, backgroundType, zIndex);
            objFullSize = createFunc(new Point()).Boundary.Size;
            if (produceMode is ProduceMode.One)
            {
                createFunc(objPoint);
            }
            else
            {
                Util.BatchRecCreate(createFunc, objPoint, quantity, objFullSize, ignoredSet);
            }
            return true;
        }

        #endregion

        #endregion
    }
}

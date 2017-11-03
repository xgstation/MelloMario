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

namespace MelloMario.LevelGen
{
    class GameEntityConverter : JsonConverter
    {
        private static readonly IEnumerable<Type> assemblyTypes = from type in Assembly.GetExecutingAssembly().GetTypes() where typeof(IGameObject).IsAssignableFrom(type) select type;
        private static readonly IEnumerable<Type> blockTypes = from type in assemblyTypes where type.Namespace == "MelloMario.BlockObjects" select type;
        private static readonly IEnumerable<Type> ItemTypes = from type in assemblyTypes where type.Namespace == "MelloMario.ItemObjects" select type;
        private static readonly IEnumerable<Type> EnemyTypes = from type in assemblyTypes where type.Namespace == "MelloMario.EnemyObjects" select type;
        private static readonly IEnumerable<Type> MiscTypes = from type in assemblyTypes where type.Namespace == "MelloMario.MiscObjects" select type;
        private IGameWorld world;
        private int grid;
        public GameEntityConverter(IGameWorld parentGameWorld, int gridSize)
        {
            world = parentGameWorld;
            grid = gridSize;
        }
        public override bool CanConvert(Type objectType)
        {
            return typeof(EncapsulatedObject<IGameObject>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken objToken = JToken.Load(reader);
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
                    break;
            }
            if (type == null)
            {
                Debug.Print("Deserialize fail: " + typeStr + " not found!");

                return null;
            }

            if (blockTypes.Contains(type))
            {
            
            }
            else if (EnemyTypes.Contains(type))
            {

            }
            else if (ItemTypes.Contains(type))
            {

            }
            else if (MiscTypes.Contains(type))
            {

            }
            else
            {
             //   Activator.CreateInstance(type, world, location);
            }



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
            JToken tempToken = token;
            for (int i = 0; i < p.Length - 1; i++)
            {
                if (tempToken[p[i]] != null)
                    tempToken = tempToken[p[i]];
                else
                    return false;
            }
            if (tempToken[p[p.Length - 1]] != null)
            {
                obj = tempToken[p[p.Length - 1]].ToObject<T>();
                return true;
            }
            else
            {
                return false;
            }
        }
        private static IList<IGameObject> CreateItemList(IGameWorld world, Point point, params string[] s)
        {
            if (s != null)
            {
                var list = new List<IGameObject>();
                for (int i = 0; i < s.Length; i++)
                {
                    list.Add(GameObjectFactory.Instance.CreateGameObject(s[i], world, point));
                }
                return list;
            }
            return null;
        }
        private void PushNewObject(Stack<IGameObject> objectStack, string type, Point location, IList<IGameObject> list, Tuple<bool, string[]> property)
        {
            IGameObject objectToPush = null;
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
                    objectToPush = new Background(world, location, type, ZIndex.background);
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

        private void SimpleBatchCreate(Type type, Point startPoint, Point quantity, Point objSize, ICollection<Point> ignore, ref Stack<BaseGameObject> stackToBeEncapsulated)
        {
            for (var x = 0; x < startPoint.X; x++)
            {
                for (var y = 0; y < startPoint.Y; y++)
                {
                    var createLocation = new Point(startPoint.X * grid + x * objSize.X, startPoint.Y * grid + y * objSize.Y);
                    if (!ignore.Contains(createLocation))
                        stackToBeEncapsulated.Push(Activator.CreateInstance(type, world, createLocation) as BaseGameObject);
                }
            }
        }


        private bool TryReadIgnoreSet(JToken token, out HashSet<Point> toBeIgnored)
        {
            toBeIgnored = new HashSet<Point>();
            if (token["Ignored"] == null) return false;
            var ignoredToken = token["Ignored"].ToList();
            foreach (var t in ignoredToken)
            {
                toBeIgnored.Add(t.ToObject<Point>());
            }
            return true;
        }
        private bool BlockConverter(Type type, JToken token, ref Stack<BaseGameObject> stackToBeEncapsulated)
        {
            object createFunction;
            var quantity = TryGet(out Point p, token, "Quantity") ? p : new Point(1, 1);
            if (quantity.X == 1 && quantity.Y == 1)
            {
            }
            else
            {
            }
            TryReadIgnoreSet(token, out var toBeIgnored);
            if (!TryGet(out Point startPoint, token, "Point"))
            {
                Debug.WriteLine("Deserialize fail: No start point provided!");
                return false;
            }
            if (typeof(Brick).IsAssignableFrom(type) || typeof(Question).IsAssignableFrom(type))
            {
                stackToBeEncapsulated.Push(Activator.CreateInstance(type, world, startPoint) as BaseGameObject);
            }
            else if (typeof(Pipeline).IsAssignableFrom(type))
            {

            }
            else
            {
                stackToBeEncapsulated.Push(Activator.CreateInstance(type) as BaseGameObject);
                return true;
            }
            return false;
        }
    }
}

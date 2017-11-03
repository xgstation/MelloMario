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

namespace MelloMario.LevelGen
{
    class GameEntityConverter : JsonConverter
    {
        private IGameWorld gameWorld;
        private int grid;
        public GameEntityConverter(IGameWorld parentGameWorld, int gridSize)
        {
            gameWorld = parentGameWorld;
            grid = gridSize;
        }
        public override bool CanConvert(Type objectType)
        {
            return typeof(EncapsulatedObject<IGameObject>).IsAssignableFrom(objectType) || objectType is IGameObject;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken objToken = JToken.Load(reader);
            var objectStack = new Stack<IGameObject>();

            string typeStr = TryGet<string>(objToken, "Type");
            Type type = null;
            var assemblyTypes = AppDomain.CurrentDomain.GetAssemblies()[1].GetTypes();
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





            var startPoint = TryGet<Point>(objToken, "Point");

            //Read quantity of object
            //If "Quantity" does not exist, default value is 1
            int rows = 1;
            int columns = 1;
            if (objToken["Quantity"] != null)
            {
                rows = TryGet<int>(objToken, "Quantity", "X");
                columns = TryGet<int>(objToken, "Quantity", "Y");
            }

            //Read object properties if given
            IDictionary<Point, Tuple<bool, string[]>> Properties = null;
            var propertiesJToken = objToken["Properties"];
            if (propertiesJToken != null && propertiesJToken.HasValues)
            {
                Properties = propertiesJToken.ToDictionary(
                  token => TryGet<Point>(token, "Index"),
                  token => new Tuple<bool, string[]>(
                      TryGet<bool>(token, "isHidden"),
                      TryGet<string[]>(token, "ItemValue")));
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
                        IList<IGameObject> list = new List<IGameObject>();

                        if (Properties == null || !Properties.TryGetValue(new Point(i, j), out Tuple<bool, string[]> property))
                        {
                            property = new Tuple<bool, string[]>(false, null);
                        }
                        else
                        {
                            list = CreateItemList(gameWorld, location, property.Item2);
                        }
                        PushNewObject(objectStack, typeStr, location, list, property);
                    }
                }
            }
            return new EncapsulatedObject<IGameObject>(objectStack);
        }
        //TODO: Add serialize method and change CanWrite 
        public override bool CanWrite { get { return false; } }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //TODO: Implement serializer
        }

        static private T TryGet<T>(JToken token, params string[] p)
        {
            T obj = default(T);
            JToken tempToken = token;
            for (int i = 0; i < p.Length - 1; i++)
            {
                if (tempToken[p[i]] != null)
                    tempToken = tempToken[p[i]];
            }
            if (tempToken[p[p.Length - 1]] != null)
            {
                obj = tempToken[p[p.Length - 1]].ToObject<T>();
            }
            return obj;
        }
        static private IList<IGameObject> CreateItemList(IGameWorld world, Point point, params string[] s)
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
                    objectToPush = new Brick(gameWorld, location, list, property.Item1);
                    break;
                case "Question":
                    objectToPush = new Question(gameWorld, location, list, property.Item1);
                    break;
                case "Pipeline":
                    objectToPush = new Pipeline(gameWorld, location, property.Item2[0]);
                    break;
                case "Floor":
                    objectToPush = new Floor(gameWorld, location);
                    break;
                case "Stair":
                    objectToPush = new Stair(gameWorld, location);
                    break;
                case "ShortCloud":
                    objectToPush = new Background(gameWorld, location, type, ZIndex.background3);
                    break;
                case "ShortSmileCloud":
                    objectToPush = new Background(gameWorld, location, type, ZIndex.background);
                    break;
                case "LongCloud":
                    objectToPush = new Background(gameWorld, location, type, ZIndex.background1);
                    break;
                case "LongSmileCloud":
                    objectToPush = new Background(gameWorld, location, type, ZIndex.background2);
                    break;
                default:
                    objectToPush = GameObjectFactory.Instance.CreateGameObject(type, gameWorld, location);
                    break;
            }
            if (objectToPush != null)
            {
                objectStack.Push(objectToPush);
            }
        }
    }
}

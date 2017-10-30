using MelloMario.BlockObjects;
using MelloMario.ItemObjects;
using MelloMario.Factories;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MelloMario.LevelGen
{
    class BaseGameObjectConverter : JsonConverter
    {
        private GameWorld gameWorld;
        private int grid;
        public BaseGameObjectConverter(GameWorld parentGameWorld, int gridSize)
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

            var type = tryGet<string>(objToken, "Type");
            var startPoint = tryGet<Point>(objToken, "Point");
            int rows = 1;
            int columns = 1;
            if (objToken["Quantity"] != null)
            {
                rows = tryGet<int>(objToken, "Quantity", "X");
                columns = tryGet<int>(objToken, "Quantity", "Y");
            }
            IDictionary<Point, Tuple<bool, string[]>> Properties = null;
            var propertiesJToken = objToken["Properties"];
            if (propertiesJToken != null && propertiesJToken.HasValues)
            {
                Properties = propertiesJToken.ToDictionary(
                  token => tryGet<Point>(token, "Index"),
                  token => new Tuple<bool, string[]>(
                      tryGet<bool>(token, "isHidden"),
                      tryGet<string[]>(token, "ItemValue")));
            }
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Point location = new Point((startPoint.X + i) * grid, (startPoint.Y + j) * grid);
                    Tuple<bool, string[]> property = null;
                    IList<IGameObject> list = null;
                    if (Properties == null || !Properties.TryGetValue(new Point(i, j), out property))
                    {
                        property = new Tuple<bool, string[]>(false, null);
                    }
                    else
                    {
                        list = createItemList(gameWorld, location, property.Item2);
                    }
                    switch (type)
                    {
                        //TODO: Add items parameter in constructors for Brick and Question
                        case "Brick":
                            if (list != null)
                            {
                                objectStack.Push(new Brick(gameWorld, location, list, property.Item1));
                            }
                            else
                            {
                                objectStack.Push(new Brick(gameWorld, location, property.Item1));
                            }
                            break;
                        case "Question":
                            if (list != null)
                            {
                                objectStack.Push(new Question(gameWorld, location, list, property.Item1));
                            }
                            else
                            {
                                objectStack.Push(new Question(gameWorld, location));
                            }
                            break;
                        case "Pipeline":
                            objectStack.Push(new Pipeline(gameWorld, location, property.Item2[0]));
                            break;
                        case "Floor":
                            objectStack.Push(new Floor(gameWorld, location));
                            break;
                        case "Stair":
                            objectStack.Push(new Stair(gameWorld, location));
                            break;
                        default:
                            objectStack.Push(GameObjectFactory.Instance.CreateGameObject(type, gameWorld, location));
                            break;
                    }
                }
            }
            return new EncapsulatedObject<IGameObject>(objectStack);
        }
        //TODO: Add serialize method and change CanWrite 
        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private T tryGet<T>(JToken token, params string[] p)
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
        private IList<IGameObject> createItemList(GameWorld world, Point point, params string[] s)
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
    }
}

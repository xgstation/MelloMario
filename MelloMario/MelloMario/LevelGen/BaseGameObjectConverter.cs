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
        private GameWorld2 gameWorld;

        public BaseGameObjectConverter(GameWorld2 gameWorld)
        {
            this.gameWorld = gameWorld;
        }
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public IEnumerable<IGameObject> GenerateItems(Tuple<bool, string> property, IGameWorld world, Point location)
        {
            // note: just yield return all items here
            //       move this function to some other class if needed
            yield return new Coin(world, location);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken gameObjToken = JToken.Load(reader);
            string gameObjectType = gameObjToken.ElementAt(0).First.ToObject<string>();
            Point startPoint = gameObjToken.ElementAt(1).First.ToObject<Point>();
            int rows = gameObjToken.ElementAt(2).First.ElementAt(0).ToObject<int>();
            int columns = gameObjToken.ElementAt(2).First.ElementAt(1).ToObject<int>();
            IDictionary<Point, Tuple<bool, string>> Properties =
                gameObjToken.ElementAt(3).ToDictionary(
                    token => token.ElementAt(0).ToObject<Point>(),
                    token => new Tuple<bool, string>(token.First.ElementAt(1).ToObject<bool>(),
                                                        token.First.ElementAt(2).ToObject<string>()));
            var objectStack = new Stack<IGameObject>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Point location = new Point(startPoint.X + i, startPoint.Y + j);
                    Tuple<bool, string> property;
                    if (!Properties.TryGetValue(new Point(i, j), out property))
                    {
                        property = new Tuple<bool, string>(false, "");
                    }
                    switch (gameObjectType)
                    {
                        case "Brick":
                            objectStack.Push(new Brick(gameWorld, location, GenerateItems(property, gameWorld, location)));
                            break;
                        case "Question":
                            objectStack.Push(new Question(gameWorld, location, GenerateItems(property, gameWorld, location)));
                            break;
                        case "Pipeline":
                            objectStack.Push(new Pipeline(gameWorld, location, property.Item2));
                            break;
                        case "Floor":
                            objectStack.Push(new Floor(gameWorld, location));
                            break;
                        case "Stair":
                            objectStack.Push(new Stair(gameWorld, location));
                            break;
                        default:
                            objectStack.Push(GameObjectFactory.Instance.CreateGameObject(gameObjectType, gameWorld, location));
                            break;
                    }
                }
            }
            if (rows == 1 && columns == 1)
            {
                return objectStack.Pop();
            }
            return objectStack;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

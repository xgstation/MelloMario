using MelloMario.MarioObjects;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace MelloMario.LevelGen
{
    class CharacterConverter : JsonConverter
    {
        private GameWorld gameWorld;
        private int grid;
        public CharacterConverter(GameWorld gameWorld, int gridSize)
        {
            this.gameWorld = gameWorld;
            grid = gridSize;
        }
        public override bool CanConvert(Type objectType)
        {
            return typeof(EncapsulatedObject<PlayerMario>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jsonToken = JToken.Load(reader);
            var characterStack = new Stack<PlayerMario>();
            var startPoint = jsonToken["SpawnPoint"].ToObject<Point>();
            var state = jsonToken["State"].ToObject<string>();
            startPoint.X = startPoint.X * grid;
            startPoint.Y = startPoint.Y * grid;
            var mario = new PlayerMario(gameWorld, startPoint);
            characterStack.Push(mario);
            //TODO: Change with IDictionary to change the state of each characters
            switch (state)
            {
                //TODO: Finish switch statement
                case "FireMario":
                    mario.UpgradeToFire();
                    break;
                case "SuperMario":
                    mario.UpgradeToSuper();
                    break;
                default:
                    //Do nothing
                    break;
            }
            return new EncapsulatedObject<PlayerMario>(characterStack);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

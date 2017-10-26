using MelloMario.MarioObjects;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.LevelGen
{
    class CharacterConverter : JsonConverter
    {
        private GameWorld gameWorld;
        public CharacterConverter(GameWorld gameWorld)
        {
            this.gameWorld = gameWorld;
        }
        public override bool CanConvert(Type objectType)
        {
            return typeof(EncapsulatedObject<PlayerMario>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jsonToken = JToken.Load(reader);
            var characterStack = new Stack<PlayerMario>();
            string characterType = jsonToken.ElementAt(0).First.ToObject<string>();
            Point startPoint = jsonToken.ElementAt(1).First.ToObject<Point>();
            //TODO: Change with Grid Scale
            startPoint.X = startPoint.X * 32;
            startPoint.Y = startPoint.Y * 32;
            characterStack.Push(new PlayerMario(gameWorld, startPoint));
            string state = jsonToken.ElementAt(2).First.ToObject<string>();
            //TODO: Change with IDictionary to change the state of each characters
            switch (state)
            {
                //TODO: Finish switch statement
                case "FireMario":
                    //mario.UpgradeToFire();
                    break;
                case "SuperMario":
                    //mario.UpgradeToSuper();
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

using MelloMario.MarioObjects;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.LevelGen
{
    class CharacterConverter : JsonConverter
    {
        private string index;
        private GameWorld2 gameWorld;
        public CharacterConverter(string index, GameWorld2 gameWorld)
        {
            this.index = index;
            this.gameWorld = gameWorld;
        }
        public override bool CanConvert(Type objectType)
        {

            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //TODO: CHANGE IT
            return new Mario(gameWorld, new Point(3, 3));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

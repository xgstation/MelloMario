﻿using MelloMario.MarioObjects;
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
        private GameWorld2 gameWorld;
        public CharacterConverter(GameWorld2 gameWorld)
        {
            this.gameWorld = gameWorld;
        }
        public override bool CanConvert(Type objectType)
        {

            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jsonToken = JToken.Load(reader);
            string characterType = jsonToken.ElementAt(0).First.ToObject<string>();
            Point startPoint = jsonToken.ElementAt(1).First.ToObject<Point>();
            Mario mario = new Mario(gameWorld, startPoint);
            switch (jsonToken.ElementAt(2).Value<string>())
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
            return mario;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
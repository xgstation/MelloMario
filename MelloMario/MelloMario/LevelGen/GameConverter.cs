using MelloMario.Containers;
using MelloMario.MarioObjects;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MelloMario.LevelGen
{
    //Using for deserialize json to a single GameWorld(Map)
    class GameConverter : JsonConverter
    {
        private string index;
        private JsonSerializer serializers;

        private IGameWorld world;
        private IPlayer character;

        public GameConverter(string index)
        {
            this.index = index;
            serializers = new JsonSerializer();
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jsonToken = JToken.Load(reader);
            JToken MapList = jsonToken.Value<JToken>("Maps");
            JToken MapToBeLoaded = null;
            //Get item "Maps"
            foreach (JToken obj in MapList)
            {
                if (obj.Value<string>("Index") == index)
                {
                    MapToBeLoaded = obj;
                    break;
                }
            }
            Point mapSize = MapToBeLoaded.Value<JToken>("Size").ToObject<Point>();
            int grid = MapToBeLoaded["Grid"].ToObject<int>();

            IList<JToken> Structures = MapToBeLoaded.Value<JToken>("Entity").ToList();
            world = new GameWorld(mapSize);
            serializers.Converters.Add(new GameEntityConverter(world, grid));
            serializers.Converters.Add(new CharacterConverter(world, grid));

            foreach (JToken jToken in Structures)
            {
                EncapsulatedObject<IGameObject> gameObjs = jToken.ToObject<EncapsulatedObject<IGameObject>>(serializers);
                if (gameObjs != null)
                {
                    foreach (IGameObject gameObj in gameObjs.RealObj)
                    {
                        world.Add(gameObj);
                    }
                }
            }

            IList<JToken> Characters = MapToBeLoaded.Value<JToken>("Characters").ToList();
            foreach (JToken obj in Characters)
            {
                EncapsulatedObject<PlayerMario> temp = obj.ToObject<EncapsulatedObject<PlayerMario>>(serializers);
                PlayerMario mario = temp.RealObj.Pop();
                character = mario;
                world.Add(mario);
                //TODO: Add support for IEnumerables<IGameCharacter> for Multi Players\
            }
            return new Tuple<IGameWorld, IPlayer>(world, character);
        }

        //TODO: Add serialize method and change CanWrite 
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //TODO: Implement serializer
        }
    }
}

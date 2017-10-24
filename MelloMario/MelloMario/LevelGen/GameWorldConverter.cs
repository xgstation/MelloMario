using MelloMario.Factories;
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
    //Using for deserialize json to a single GameWorld(Map)
    class GameWorld2Converter : JsonConverter
    {
        private string index;
        private GameWorld2 gameWorld;
        private GameModel2 gameModel;
        JsonSerializer serializers;
        public string Index
        {
            get
            {
                return index;
            }
            set
            {
                value = index;
            }
        }
        public GameWorld2Converter(string index, GameModel2 gameModel)
        {
            this.index = index;
            this.gameModel = gameModel;
            serializers = new JsonSerializer();
            serializers.Converters.Add(new BaseGameObjectConverter(gameWorld));
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
            foreach (var obj in MapList)
            {
                if (obj.Value<String>("Index") == index)
                {
                    MapToBeLoaded = obj;
                    break;
                }
            }
            Point mapSize = MapToBeLoaded.Value<JToken>("Size").ToObject<Point>();

            IList<JToken> Structures = MapToBeLoaded.Value<JToken>("Structures").ToList();
            gameWorld = new GameWorld2(MapToBeLoaded.Value<int>("Grid"), mapSize, gameModel);

            foreach (var obj in Structures)
            {
                var temp = obj.ToObject<object>(serializers);
                if (temp is IGameObject gameObject)
                {
                    gameWorld.AddObject(gameObject);
                }
                else if (temp is IEnumerable<IGameObject> gameObjects)
                {
                    foreach (var gameObj in gameObjects)
                    {
                        gameWorld.AddObject(gameObj);
                    }
                }
            }
            return gameWorld;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

}

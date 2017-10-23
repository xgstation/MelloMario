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
    class GameWorld2Converter : JsonConverter
    {
        private string index;
        private GameModel2 gameModel;
        JsonSerializer serializers;
        IGameObjectFactory factory = GameObjectFactory.Instance;
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
            serializers.Converters.Add(new CharacterConverter());
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType is IGameWorld;
        }
        private void matrixAdd(JToken token, IGameWorld world)
        {

        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            JToken MapList;
            JToken MapToBeLoaded = null;
            //Get item "Maps"
            if (jsonObject.TryGetValue("Maps", out MapList))
            {
                foreach (var obj in MapList)
                {
                    if (obj.Value<String>("Index") == index)
                    {
                        MapToBeLoaded = obj;
                        break;
                    }
                }
            }
            else
            {
                //Print: json contains no maps or invalid format
            }

            IList<JToken> Structures = MapToBeLoaded.Values<JToken>("Structures").ToList();
            IGameWorld world = new GameWorld2(MapToBeLoaded.Value<int>("Grid"), 
                MapToBeLoaded.Value<Point>("Size"), gameModel);

            foreach (var obj in Structures)
            {
                world.AddObject(obj.ToObject<IGameObject>(serializers));
            }
            return world;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

}

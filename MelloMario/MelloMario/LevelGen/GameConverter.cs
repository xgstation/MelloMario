using MelloMario.Containers;
using MelloMario.MarioObjects;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.LevelGen
{
    //Using for deserialize json to a single GameWorld(Map)
    class GameConverter : JsonConverter
    {
        private GameEntityConverter gameEntityConverter;
        private GameModel model;
        private IGameSession session;
        private JToken jsonToken;
        private JToken MapListToken;
        private JToken MapToBeLoaded;
        private string index;
        private static JsonSerializer serializers;

        private IGameWorld world;
        private Listener listener;

        public GameConverter(GameModel model, IGameSession session, Listener listener, string index = "Main")
        {
            this.model = model;
            this.session = session;
            this.index = index;
            this.listener = listener;
            serializers = new JsonSerializer();
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            jsonToken = JToken.Load(reader);
            MapListToken = Util.TryGet(out JToken t, jsonToken, "Maps") ? t : null;
            if (MapListToken == null)
            {
                return null;
            }
            MapToBeLoaded = null;
            foreach (JToken obj in MapListToken)
            {
                if (Util.TryGet(out string s, obj, "Index") && s == index)
                {
                    MapToBeLoaded = obj;
                    break;
                }
            }
            Util.TryGet(out string MapType, MapToBeLoaded, "Type");
            if (Util.TryGet(out Point mapSize, MapToBeLoaded, "Size"))
            {
                Debug.WriteLine("Map size:" + mapSize);
            }
            else
            {
                Debug.WriteLine("Deserialize fail: No map size provided!");
            }

            if (Util.TryGet(out IList<JToken> entities, MapToBeLoaded, "Entity"))
            {
                Debug.WriteLine("Entities token loaded successfully!");
            }

            Util.TryGet(out Point initialPoint, MapToBeLoaded, "InitialSpawnPoint");
            Util.TryGet(out IList<Point> respawnPoints, MapToBeLoaded, "RespawnPoints");
            world = new GameWorld(index, mapSize, initialPoint, respawnPoints);

            gameEntityConverter = new GameEntityConverter(model, world, listener);

            serializers.Converters.Add(gameEntityConverter);
            if (entities != null)
            {
                foreach (JToken jToken in entities)
                {
                    jToken.ToObject<EncapsulatedObject<IGameObject>>(serializers);
                }
            }

            return world;
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

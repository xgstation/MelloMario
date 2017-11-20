using System;
using System.Collections.Generic;
using System.Diagnostics;
using MelloMario.Containers;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MelloMario.LevelGen
{
    //Using for deserialize json to a single GameWorld(Map)
    internal class GameConverter : JsonConverter
    {
        private static JsonSerializer Serializers;
        private readonly string index;
        private readonly IListener listener;
        private readonly GameModel model;
        private GameEntityConverter gameEntityConverter;
        private JToken jsonToken;
        private JToken mapListToken;
        private JToken mapToBeLoaded;

        private IGameWorld world;

        public GameConverter(GameModel model, IListener listener, string index = "Main")
        {
            this.model = model;
            this.index = index;
            this.listener = listener;
            Serializers = new JsonSerializer();
        }

        //TODO: Add serialize method and change CanWrite 
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            jsonToken = JToken.Load(reader);
            mapListToken = Util.TryGet(out JToken t, jsonToken, "Maps") ? t : null;
            if (mapListToken == null)
            {
                return null;
            }
            mapToBeLoaded = null;
            foreach (JToken obj in mapListToken)
            {
                if (Util.TryGet(out string s, obj, "Index") && s == index)
                {
                    mapToBeLoaded = obj;
                    break;
                }
            }
            Util.TryGet(out string mapType, mapToBeLoaded, "Type");
            if (Util.TryGet(out Point mapSize, mapToBeLoaded, "Size"))
            {
                Debug.WriteLine("Map size:" + mapSize);
            }
            else
            {
                Debug.WriteLine("Deserialize fail: No map size provided!");
            }

            if (Util.TryGet(out IList<JToken> entities, mapToBeLoaded, "Entity"))
            {
                Debug.WriteLine("Entities token loaded successfully!");
            }

            Util.TryGet(out Point initialPoint, mapToBeLoaded, "InitialSpawnPoint");
            Util.TryGet(out IList<Point> respawnPoints, mapToBeLoaded, "RespawnPoints");
            world = new GameWorld(index, mapSize, new Point(2, 2), respawnPoints);

            gameEntityConverter = new GameEntityConverter(model, world, listener);

            Serializers.Converters.Add(gameEntityConverter);
            if (entities == null)
            {
                return world;
            }
            foreach (JToken jToken in entities)
            {
                jToken.ToObject<EncapsulatedObject<IGameObject>>(Serializers);
            }

            return world;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //TODO: Implement serializer
        }
    }
}

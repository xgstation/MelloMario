namespace MelloMario.LevelGen.JsonConverters
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using MelloMario.Containers;
    using Microsoft.Xna.Framework;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    #endregion

    //Using for deserialize json to a single GameWorld(Map)
    internal class GameConverter : JsonConverter
    {
        private static JsonSerializer Serializers;
        private readonly string id;
        private readonly IListener<IGameObject> listener;
        private readonly IListener<ISoundable> soundListener;
        private GameEntityConverter gameEntityConverter;
        private JToken jsonToken;
        private JToken mapListToken;
        private JToken mapToBeLoaded;

        private IWorld world;
        private Static generator;

        public GameConverter(
            IWorld world,
            Static generator,
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener,
            string id = "Main")
        {
            this.world = world;
            this.generator = generator;
            this.id = id;
            this.listener = listener;
            this.soundListener = soundListener;
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

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
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
                if (Util.TryGet(out string s, obj, "Index") && s == id)
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
            respawnPoints.Add(initialPoint);

            // TODO: separate world initialization and object loading
            //IWorld world = new World(
            //    id,
            //    mapType == "Normal" ? WorldType.normal : WorldType.underground,
            //    generator, // TODO: inverse the dependency
            //    respawnPoints);

            gameEntityConverter = new GameEntityConverter(world, listener, soundListener);

            Serializers.Converters.Add(gameEntityConverter);

            foreach (JToken jToken in entities)
            {
                EncapsulatedObject<IGameObject> objs = jToken.ToObject<EncapsulatedObject<IGameObject>>(Serializers);
                if (objs != null)
                {
                    generator.FeedObjects(objs.RealObj);
                }
            }

            return world;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //TODO: Implement serializer
        }
    }
}

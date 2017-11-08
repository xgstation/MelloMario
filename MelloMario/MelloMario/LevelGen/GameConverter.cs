using MelloMario.Containers;
using MelloMario.MarioObjects;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.LevelGen
{
    //Using for deserialize json to a single GameWorld(Map)
    class GameConverter : JsonConverter
    {
        private GameEntityConverter gameEntityConverter;
        private CharacterConverter characterConverter;
        private GraphicsDevice graphicsDevice;
        private GameModel model;
        private JToken jsonToken;
        private JToken MapListToken;
        private JToken MapToBeLoaded;
        private string index;
        private static JsonSerializer serializers;

        private IGameWorld world;
        private IPlayer character;

        public GameConverter(GameModel model, GraphicsDevice graphicsDevice, string index = "Main")
        {
            this.model = model;
            this.graphicsDevice = graphicsDevice;
            this.index = index;
            serializers = new JsonSerializer();
        }

        public void SetIndex(string index)
        {
            this.index = index;
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
            if (MapListToken == null) return null;
            MapToBeLoaded = null;
            foreach (JToken obj in MapListToken)
            {
                if (Util.TryGet(out string s, obj, "Index") && s == index)
                {
                    MapToBeLoaded = obj;
                    break;
                }
            }
            if (Util.TryGet(out Point mapSize, MapToBeLoaded, "Size"))
            {
                Debug.WriteLine("Map size:" + mapSize);
            }
            else
            {
                Debug.WriteLine("Deserialize fail: No map size provided!");
            }
            if (Util.TryGet(out int grid, MapToBeLoaded, "Grid"))
            {
                Debug.WriteLine("Grid size:" + grid);
            }
            else
            {
                grid = 32;
                Debug.WriteLine("No grid size provided, using default value: 32.");
            }

            if (Util.TryGet(out IList<JToken> entities, MapToBeLoaded, "Entity"))
            {
                Debug.WriteLine("Entities token loaded successfully!");
            }

            Util.TryGet(out Point initialPoint, MapToBeLoaded, "InitialSpawnPoint");
            Util.TryGet(out IList<Point> respawnPoints, MapToBeLoaded, "RespawnPoints");
            world = new GameWorld(mapSize, initialPoint, respawnPoints);

            gameEntityConverter = new GameEntityConverter(model, graphicsDevice, world, grid);

            characterConverter = new CharacterConverter(world, grid);

            serializers.Converters.Add(gameEntityConverter);
            serializers.Converters.Add(characterConverter);
            if (entities != null)
            {
                foreach (JToken jToken in entities)
                {
                    EncapsulatedObject<IGameObject> gameObjs =
                        jToken.ToObject<EncapsulatedObject<IGameObject>>(serializers);

                    if (gameObjs != null)
                    {
                        foreach (IGameObject gameObj in gameObjs.RealObj)
                        {
                            world.Add(gameObj);
                        }
                    }

                }

                if (Util.TryGet(out IList<JToken> characters, MapToBeLoaded, "Characters"))
                {
                    Debug.WriteLine("Characters token loaded successfully!");
                }
                if (characters != null)
                {
                    foreach (JToken obj in characters)
                    {
                        EncapsulatedObject<PlayerMario> temp =
                            obj.ToObject<EncapsulatedObject<PlayerMario>>(serializers);

                        PlayerMario mario = temp.RealObj.Pop();
                        character = mario;
                        world.Add(mario);

                        //TODO: Add support for IEnumerables<IGameCharacter> for Multi Players\
                    }
                }
            }

            //if (character == null) return world;
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

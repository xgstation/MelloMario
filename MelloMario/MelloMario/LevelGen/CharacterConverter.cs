using MelloMario.Factories;
using MelloMario.MarioObjects;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace MelloMario.LevelGen
{
    class CharacterConverter : JsonConverter
    {
        private JToken jsonToken;
        private IGameSession gameSession;
        private IGameWorld gameWorld;
        private Stack<PlayerMario> characterStack;
        private Point startPoint;
        private string state;
        private int grid;
        private Listener listener;

        public CharacterConverter(IGameSession gameSession, IGameWorld gameWorld, Listener listener, int gridSize)
        {
            this.gameSession = gameSession;
            this.gameWorld = gameWorld;
            this.listener = listener;
            grid = gridSize;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(EncapsulatedObject<PlayerMario>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            jsonToken = JToken.Load(reader);
            characterStack = new Stack<PlayerMario>();
            startPoint = jsonToken["SpawnPoint"].ToObject<Point>();
            state = jsonToken["State"].ToObject<string>();
            startPoint.X = startPoint.X * grid;
            startPoint.Y = startPoint.Y * grid;

            characterStack.Push((PlayerMario) GameObjectFactory.Instance.CreateGameCharacter("Mario", gameSession, gameWorld, startPoint, listener).Item1);

            return new EncapsulatedObject<PlayerMario>(characterStack);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

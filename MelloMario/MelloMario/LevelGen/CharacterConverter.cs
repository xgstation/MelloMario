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
        private Stack<MarioCharacter> characterStack;
        private Point startPoint;
        private Listener listener;

        public CharacterConverter(IGameSession gameSession, IGameWorld gameWorld, Listener listener)
        {
            this.gameSession = gameSession;
            this.gameWorld = gameWorld;
            this.listener = listener;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(EncapsulatedObject<MarioCharacter>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            jsonToken = JToken.Load(reader);
            characterStack = new Stack<MarioCharacter>();
            startPoint = jsonToken["SpawnPoint"].ToObject<Point>();
            startPoint.X = startPoint.X * GameConst.GRID;
            startPoint.Y = startPoint.Y * GameConst.GRID;

            characterStack.Push((MarioCharacter) GameObjectFactory.Instance.CreateGameCharacter("Mario", gameSession, gameWorld, startPoint, listener).Item1);

            return new EncapsulatedObject<MarioCharacter>(characterStack);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

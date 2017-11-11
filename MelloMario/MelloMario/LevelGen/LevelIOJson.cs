using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using MelloMario.Theming;

namespace MelloMario.LevelGen
{
    class LevelIOJson
    {
        // Note: As File.ReadAllText will take care of dispose itself,
        // there is no need to implement IDisposable

        private string path;
        private GameModel model;
        private string levelString;
        private GraphicsDevice graphicsDevice;
        private GameConverter gameConverter;
        private Listener listener;

        public LevelIOJson(string jsonPath, GraphicsDevice graphicsDevice, Listener listener)
        {
            this.graphicsDevice = graphicsDevice;
            this.listener = listener;
            path = jsonPath;
        }

        public void SetModel(GameModel model)
        {
            this.model = model;
        }

        public Tuple<IGameWorld, IPlayer> Load(string index, IGameSession session)
        {
            levelString = File.ReadAllText(path);
            gameConverter = new GameConverter(model, session, graphicsDevice, listener, index);
            return JsonConvert.DeserializeObject<Tuple<IGameWorld, IPlayer>>(levelString, gameConverter);

        }
    }
}

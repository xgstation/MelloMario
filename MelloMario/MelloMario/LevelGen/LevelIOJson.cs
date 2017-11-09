using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using MelloMario.Theming;

namespace MelloMario.LevelGen
{
    class LevelIOJson
    {
        // Note: Without implementing IDisposable, it may cause resource leak
        //       The code analysis tool generates a warning here

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

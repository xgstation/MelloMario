using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace MelloMario.LevelGen
{
    class LevelIOJson : IDisposable
    {
        // Note: Without implementing IDisposable, it may cause resource leak
        //       The code analysis tool generates a warning here

        private string path;
        private GameModel model;
        private string levelString;
        private GraphicsDevice graphicsDevice;
        private GameConverter gameConverter;
        public LevelIOJson(string jsonPath, GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            path = jsonPath;
        }

        public void SetModel(GameModel model)
        {
            this.model = model;
        }

        public Tuple<IGameWorld, IPlayer> Load(string index = "Main")
        {
            levelString = File.ReadAllText(path);
            gameConverter = new GameConverter(model, graphicsDevice, index);
            return JsonConvert.DeserializeObject<Tuple<IGameWorld, IPlayer>>(levelString, gameConverter);

        }

        public void Dispose()
        {
            levelString = null;
            path = null;
            model = null;
            graphicsDevice = null;
            gameConverter = null;
        }
    }
}

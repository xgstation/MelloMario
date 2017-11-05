﻿using System;
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
        public LevelIOJson(string jsonPath, GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            path = jsonPath;
        }

        public void SetModel(GameModel model)
        {
            this.model = model;
        }

        public Tuple<IGameWorld, IPlayer> Load(string index)
        {
            levelString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Tuple<IGameWorld, IPlayer>>(levelString, new GameConverter(model, graphicsDevice, index));
        }

        //public void Close()
        //{
        //TODO: Implement IO stream close
        //}
        public void Dispose()
        {
            //TODO: Implement dispose
        }
    }
}

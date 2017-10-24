using MelloMario.Factories;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using System.Collections;
using MelloMario.MarioObjects;

namespace MelloMario.LevelGen
{
    //WIP: This is a LevelGen class in developing for testing purpose.

    class LevelIOJson : IDisposable
    {

        // Note: Without implementing IDisposable, it may cause resource leak
        //       The code analysis tool generates a warning here
  

        private const int GRIDSIZE = 32;
        private string path;
        private string levelString;
        private GameModel2 gameModel;
        

        public LevelIOJson(string path, GameModel2 gameModel)
        {
            this.path = path;
            this.gameModel = gameModel;
        }
        public Tuple<IGameWorld, IGameCharacter> Load(string index)
        {
            levelString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Tuple<IGameWorld, IGameCharacter>>(levelString, new GameConverter(index, gameModel));
        }
        public void Close()
        {
            //TODO: Implement IO stream close
        }
        public void Dispose()
        {
            //TODO: Implement dispose
        }
        

    }
}

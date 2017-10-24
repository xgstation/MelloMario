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
        private String path;
        Stream stream;
        private StreamReader input;
        private StreamWriter output;
        private String levelString;
        private List<JToken> loadList;
        private JToken jLevelToken;
        private IGameWorld[] Scenes;
        private GameModel2 gameModel;
        

        public LevelIOJson(String path, GameModel2 gameModel)
        {
            this.path = path;
            this.gameModel = gameModel;
        }
        public Tuple<IGameWorld, IGameCharacter> Load(string index)
        {
            levelString = File.ReadAllText(path);
            var world = JsonConvert.DeserializeObject<GameWorld2>(levelString, new GameWorld2Converter(index, gameModel));
            IGameCharacter character = new PlayerMario(world, new Point(10, 10));
            //IGameCharacter character = JsonConvert.DeserializeObject<IGameCharacter>(levelString, new CharacterConverter(index,world));
            return new Tuple<IGameWorld, IGameCharacter>(world, character);
        }
        public void Close()
        {
            //TODO: Rewrite these
            if (stream != null)
                stream.Close();
            if (input != null)
                input.Close();
            if (output != null)
                output.Close();
        }
        public void Dispose()
        {
            ((IDisposable)input).Dispose();
        }
        

    }
}

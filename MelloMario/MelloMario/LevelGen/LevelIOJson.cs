using System;
using System.IO;
using Newtonsoft.Json;

namespace MelloMario.LevelGen
{

    class LevelIOJson : IDisposable
    {

        // Note: Without implementing IDisposable, it may cause resource leak
        //       The code analysis tool generates a warning here
  
            
        private string path;
        private string levelString;
        private GameModel model;
        public LevelIOJson(string jsonPath, GameModel gameModel)
        {
            path = jsonPath;
            model = gameModel;
        }
        public Tuple<IGameWorld, IGameCharacter> Load(string index)
        {
            levelString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Tuple<IGameWorld, IGameCharacter>>(levelString, new GameConverter(index, model));
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

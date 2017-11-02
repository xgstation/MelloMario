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
        private GameModel Model;

        private string levelString;
        public LevelIOJson(string jsonPath)
        {
            path = jsonPath;
        }
        public void SetModel(GameModel model)
        {
            Model = model;
        }
        public Tuple<IGameWorld, ICharacter> Load(string index)
        {
            levelString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Tuple<IGameWorld, ICharacter>>(levelString, new GameConverter(index, Model));
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

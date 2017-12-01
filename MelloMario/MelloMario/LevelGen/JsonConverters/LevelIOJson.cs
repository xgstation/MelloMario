namespace MelloMario.LevelGen.JsonConverters
{
    #region

    using System.IO;
    using MelloMario.LevelGen.Generators;
    using Newtonsoft.Json;

    #endregion

    internal class LevelIOJson
    {
        private IListener<IGameObject> scoreListener;

        private IListener<ISoundable> soundListener;
        // Note: As File.ReadAllText will take care of dispose itself,
        // there is no need to implement IDisposable

        private readonly string directoryPath;
        private GameConverter gameConverter;
        private string levelString;

        public LevelIOJson(string jsonDirectoryPath)
        {
            directoryPath = jsonDirectoryPath;
            Util.Initilalize();
        }

        public void BindScoreListener(IListener<IGameObject> newScoreListener)
        {
            scoreListener = newScoreListener;
        }

        public void BindSoundListener(IListener<ISoundable> newSoundListener)
        {
            soundListener = newSoundListener;
        }

        public void Load(string map, string id, IWorld world, Static generator)
        {
            levelString = File.ReadAllText(directoryPath + "/" + map);
            gameConverter = new GameConverter(world, generator, scoreListener, soundListener, id);

            JsonConvert.DeserializeObject<IWorld>(levelString, gameConverter);
        }

        // public void Save(string path)
        // {
        //  }
    }
}

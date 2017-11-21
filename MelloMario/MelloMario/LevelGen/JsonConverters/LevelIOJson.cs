namespace MelloMario.LevelGen.JsonConverters
{
    #region

    using System.IO;
    using Newtonsoft.Json;

    #endregion

    internal class LevelIOJson
    {
        private readonly IListener<IGameObject> listener;

        private readonly IListener<ISoundable> soundListener;
        // Note: As File.ReadAllText will take care of dispose itself,
        // there is no need to implement IDisposable

        private readonly string path;
        private GameConverter gameConverter;
        private string levelString;

        public LevelIOJson(string jsonPath, IListener<IGameObject> listener, IListener<ISoundable> soundListener)
        {
            this.listener = listener;
            this.soundListener = soundListener;
            path = jsonPath;
            Util.Initilalize();
        }

        public IWorld Load(string id)
        {
            levelString = File.ReadAllText(path);
            gameConverter = new GameConverter(listener, soundListener, id);

            return JsonConvert.DeserializeObject<IWorld>(levelString, gameConverter);
        }

        public void Save(string path)
        {
            
        }
    }
}

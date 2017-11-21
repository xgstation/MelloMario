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

        private IModel model;

        public LevelIOJson(string jsonPath, IListener<IGameObject> listener, IListener<ISoundable> soundListener)
        {
            this.listener = listener;
            this.soundListener = soundListener;
            path = jsonPath;
            Util.Initilalize();
        }

        public void SetModel(IModel newModel)
        {
            model = newModel;
        }

        public IWorld Load(string index)
        {
            levelString = File.ReadAllText(path);
            gameConverter = new GameConverter(model, listener, soundListener, index);

            return JsonConvert.DeserializeObject<IWorld>(levelString, gameConverter);
        }

        public void Save(string path)
        {
            
        }
    }
}

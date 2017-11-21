namespace MelloMario.LevelGen
{
    #region

    using System.IO;
    using MelloMario.Theming;
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

        private Model model;

        public LevelIOJson(string jsonPath, IListener<IGameObject> listener, IListener<ISoundable> soundListener)
        {
            this.listener = listener;
            this.soundListener = soundListener;
            path = jsonPath;
            Util.Initilalize();
        }

        public void SetModel(Model newModel)
        {
            model = newModel;
        }

        public IGameWorld Load(string index)
        {
            levelString = File.ReadAllText(path);
            gameConverter = new GameConverter(model, listener, soundListener, index);

            return JsonConvert.DeserializeObject<IGameWorld>(levelString, gameConverter);
        }
    }
}

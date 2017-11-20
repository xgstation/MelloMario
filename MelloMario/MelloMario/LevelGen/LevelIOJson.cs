namespace MelloMario.LevelGen
{
    #region

    using System.IO;
    using Newtonsoft.Json;
    using Theming;

    #endregion

    internal class LevelIOJson
    {
        private readonly IListener listener;
        // Note: As File.ReadAllText will take care of dispose itself,
        // there is no need to implement IDisposable

        private readonly string path;
        private GameConverter gameConverter;
        private string levelString;

        private Model model;

        public LevelIOJson(string jsonPath, IListener listener)
        {
            this.listener = listener;
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
            gameConverter = new GameConverter(model, listener, index);

            return JsonConvert.DeserializeObject<IGameWorld>(levelString, gameConverter);
        }
    }
}

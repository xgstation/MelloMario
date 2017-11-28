namespace MelloMario.Theming
{
    #region

    using System;
    using System.Collections.Generic;
    using MelloMario.Containers;
    using MelloMario.Controls.Scripts;
    using MelloMario.LevelGen.JsonConverters;
    using MelloMario.Objects.UserInterfaces;
    using MelloMario.Sounds;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    [Serializable]
    internal class GameModel : IModel
    {
        private readonly Game1 game;
        private readonly IListener<IGameObject> listener;
        private readonly IListener<ISoundable> soundListener;
        private readonly Session session;
        private readonly ScreenManager screenManager;
        private IEnumerable<IController> controllers;
        private string mapPath = "Content/Level1.json";

        public bool IsPaused { get; private set; }

        public GameModel(Game1 game)
        {
            this.game = game;
            session = new Session();
            ActivePlayer = new Player(session);
            listener = new ScoreListener(this, ActivePlayer);
            soundListener = new SoundEffectListener();
            screenManager = new ScreenManager(ActivePlayer);
            Database.Initialize(session);
        }

        public Matrix GetActiveViewMatrix
        {
            get
            {
                return ActivePlayer.Camera.GetViewMatrix(new Vector2(1f));
            }
        }

        //TODO: Change with multiplayer
        public IPlayer ActivePlayer { get; }

        public void ToggleFullScreen()
        {
            game.ToggleFullScreen();
        }

        public void Pause()
        {
            IsPaused = true;
            new PausedScript().Bind(controllers, this, ActivePlayer.Character);
            //screenManager.ScreenState = ScreenManager.State.pause;
        }

        public void Init()
        {
            Normal(); // note: this is a hack
            IsPaused = true;
            new StartScript().Bind(controllers, this, ActivePlayer.Character);
            //Splash = new GameStart(ActivePlayer); // TODO: move these constructors to the factory
            screenManager.ScreenState = ScreenManager.State.start;
            screenManager.Initialize();
        }

        public void Resume()
        {
            IsPaused = false;
            new PlayingScript().Bind(controllers, this, ActivePlayer.Character);
            screenManager.ScreenState = ScreenManager.State.inGame;
        }

        public void Reset()
        {
            // TODO: "forced" version of LoadLevel()
            game.Reset();
            Resume();
        }

        public void Quit()
        {
            game.Exit();
        }

        public void Infinite()
        {
            mapPath = "Content/Infinite.json";
            ActivePlayer.Init("Mario", LoadLevel("Main"), listener, soundListener);
            session.Add(ActivePlayer);
            IsPaused = false;
            screenManager.ScreenState = ScreenManager.State.inGame;
            Resume();
        }

        public void Normal()
        {
            mapPath = "Content/Level1.json";
            ActivePlayer.Init("Mario", LoadLevel("Main"), listener, soundListener);
            session.Add(ActivePlayer);
            IsPaused = false;
            screenManager.ScreenState = ScreenManager.State.inGame;
            Resume();
        }

        public void Update(int time)
        {
            UpdateController();
            Database.Update();
            if (IsPaused)
            {
                return;
            }

            //TODO: Pause state should not stop updating camera
            //ICollection<IGameObject> tobedrawn = new List<IGameObject>();
            //foreach (IGameObject gameObject in ActivePlayer.Character.CurrentWorld.ScanNearby(ActivePlayer.Camera.Viewport))
            //{
            //    tobedrawn.Add(gameObject);
            //}
            //screenManager.Feed(tobedrawn);
            screenManager.Update(time);
            UpdateGameObjects(time);
            UpdateContainers();
        }

        public void Draw(int time, SpriteBatch spriteBatch)
        {
            IPlayer player = ActivePlayer;

            foreach (IGameObject obj in player.Character.CurrentWorld.ScanNearby(player.Camera.Viewport))
            {
                obj.Draw(IsPaused ? 0 : time, spriteBatch);
            }
            
            screenManager.Draw(time,spriteBatch);
        }

        public void ToggleMute()
        {
            (soundListener as SoundEffectListener)?.ToggleMute();
            game.ToggleMute();
        }

        public void LoadControllers(IEnumerable<IController> newControllers)
        {
            controllers = newControllers;
        }

        public IWorld LoadLevel(string id)
        {
            foreach (IWorld world in session.ScanWorlds())
            {
                if (world.ID == id)
                {
                    return world;
                }
            }

            // IWorld newWorld = new GameWorld(id, new Point(50, 20), new Point(1, 1), new List<Point>());

            LevelIOJson reader = new LevelIOJson(mapPath, listener, soundListener);

            IWorld newWorld = reader.Load(id);
            // generator = new JsonGenerator(mapPath, id, listener, soundListener);
            //infiniteGenerator = new InfiniteGenerator(newWorld, listener);
            return newWorld;
        }

        public void Transist()
        {
            ActivePlayer.Reset("Mario", listener, soundListener);

            IsPaused = true;
            new TransistScript().Bind(controllers, this, ActivePlayer.Character);
            screenManager.ScreenState = ScreenManager.State.over;
        }

        public void TransistGameWon()
        {
            ActivePlayer.Win();

            IsPaused = true;
            new TransistScript().Bind(controllers, this, ActivePlayer.Character);

            screenManager.ScreenState = ScreenManager.State.won;
        }

        private void UpdateController()
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }
        }

        private void UpdateGameObjects(int time)
        {
            // reserved for multiplayer
            ISet<IObject> updating = new HashSet<IObject>();

            foreach (IPlayer player in session.ScanPlayers())
            {
                player.Update(time);
                foreach (IGameObject obj in player.Character.CurrentWorld.ScanNearby(player.Character.Sensing, true))
                {
                    updating.Add(obj);
                }
            }

            foreach (IObject obj in updating)
            {
                obj.Update(time);
            }
        }

        private void UpdateContainers()
        {
            session.Update();
            foreach (IWorld world in session.ScanWorlds())
            {
                world.Update();
            }
        }
    }
}

namespace MelloMario.Theming
{
    #region

    using System;
    using System.Collections.Generic;
    using MelloMario.Containers;
    using MelloMario.Controls.Scripts;

    #endregion

    [Serializable]
    internal class GameModel : IModel
    {
        private readonly Game1 game;
        private ISession session;
        private IEnumerable<IController> controllers;
        private IListener<IGameObject> scoreListener;

        public GameState State { get; private set; }
        public GameMode Mode { get; }

        public GameModel(Game1 game, GameMode mode)
        {
            this.game = game;
            State = GameState.onProgress;
            Mode = mode;
            switch (mode)
            {
                case GameMode.normal:
                    Normal();
                    break;
                case GameMode.infinite:
                    Infinite();
                    break;
                case GameMode.randomMap:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public void Initialize()
        {
            session = new Session();
            scoreListener = new ScoreListener(this, game.ActivePlayer);
            Database.Initialize(session);
        }

        public void Pause()
        {
            State = GameState.pause;
            new PausedScript().Bind(controllers, this);
        }

        public void Resume()
        {
            State = GameState.onProgress;
            new PlayingScript().Bind(controllers, game.ActivePlayer.Character);
        }

        public void Reset()
        {
            game.Reset();
            Resume();
        }

        private void Infinite()
        {
            //mapPath = "Content/Infinite.json";
            game.ActivePlayer.InitCharacter("Mario", LoadLevel("Main"), scoreListener);
            session.Add(game.ActivePlayer);
            State = GameState.onProgress;
            Resume();
        }

        private void Normal()
        {
            //mapPath = "Content/Level1.json";
            game.ActivePlayer.InitCharacter("Mario", LoadLevel("Main"), scoreListener);
            session.Add(game.ActivePlayer);
            State = GameState.onProgress;
            Resume();
        }

        public void Update(int time)
        {
            UpdateController();
            Database.Update();
            if (State == GameState.pause)
            {
                return;
            }
            UpdateGameObjects(time);
            UpdateContainers();
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

            //LevelIOJson reader = new LevelIOJson(mapPath, scoreListener, soundListener);

            //IWorld newWorld = reader.Load(id);
            // generator = new JsonGenerator(mapPath, id, scoreListener, soundListener);
            //infiniteGenerator = new InfiniteGenerator(newWorld, scoreListener);
            return null;
        }

        public void Transist()
        {
            // ActivePlayer.Reset("Mario", scoreListener, soundListener);
            State = GameState.transist;
            new TransistScript().Bind(controllers, this);
        }

        public void TransistGameWon()
        {
            game.ActivePlayer.Win();
            State = GameState.transist;
            new TransistScript().Bind(controllers, this);
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
            ISet<IGameObject> updating = new HashSet<IGameObject>();

            foreach (IPlayer player in session.ScanPlayers())
            {
                player.Update(time);
                foreach (IGameObject obj in player.Character.CurrentWorld.ScanNearby(player.Character.Sensing, true))
                {
                    updating.Add(obj);
                }
            }

            foreach (IGameObject obj in updating)
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

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
        private readonly ISession session;
        private readonly IEnumerable<IController> controllers;
        private readonly IListener<IGameObject> scoreListener;
        private readonly string map;
        public GameState State { get; private set; }
        public GameMode Mode { get; }

        public GameModel(Game1 game, IEnumerable<IController> controllers, GameMode mode)
        {
            this.game = game;
            this.controllers = controllers;
            session = new Session();
            scoreListener = new ScoreListener(this, game.ActivePlayer);
            game.LevelIOJson.BindScoreListener(scoreListener);
            Database.Initialize(session);
            State = GameState.onProgress;
            Mode = mode;
            switch (mode)
            {
                case GameMode.normal:
                    map = "Level1.json";
                    Initialize();
                    break;
                case GameMode.infinite:
                    map = "Infinite.json";
                    Initialize();
                    break;
                case GameMode.randomMap:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
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

        private void Initialize()
        {
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

        public IWorld LoadLevel(string id)
        {
            foreach (IWorld world in session.ScanWorlds())
            {
                if (world.ID == id)
                {
                    return world;
                }
            }
            IWorld newWorld = game.LevelIOJson.Load(map, id);
            return newWorld;
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

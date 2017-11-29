namespace MelloMario.Theming
{
    #region

    using System;
    using System.Collections.Generic;
    using MelloMario.Containers;
    using MelloMario.Controls.Scripts;
    using MelloMario.LevelGen;
    using MelloMario.LevelGen.Terrains;
    using MelloMario.Sounds.Effects;
    using Microsoft.Xna.Framework;

    #endregion

    [Serializable]
    internal class GameModel : IModel
    {
        private readonly Game1 game;
        private readonly ISession session;
        private readonly IEnumerable<IController> controllers;
        private readonly IListener<IGameObject> scoreListener;
        private string map;

        private GameState gameState;
        private int stateTimer;

        public event EventHandler<GameState> StateChanged;

        public GameModel(Game1 game, IEnumerable<IController> controllers)
        {
            this.game = game;
            this.controllers = controllers;

            session = new Session();
            scoreListener = new ScoreListener(this, game.ActivePlayer);
            game.LevelIOJson.BindScoreListener(scoreListener);
            Database.Initialize(session);
        }

        public GameState State
        {
            get
            {
                return gameState;
            }
            private set
            {
                if (gameState != value)
                {
                    gameState = value;
                    StateChanged?.Invoke(this, State);
                }
            }
        }
        public GameMode Mode { get; private set; }

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

        public void Update(int time)
        {
            UpdateController();
            Database.Update();
            if (State == GameState.pause)
            {
                return;
            }
            UpdateState(time);
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

            // note: use static generator for normal game
            if (Mode == GameMode.normal)
            {
                Static generator = new Static();

                IWorld newWorld = new World(
                    id,
                    WorldType.normal,
                    generator,
                    new List<Point>
                    {
                        new Point()
                    });

                game.LevelIOJson.Load(map, id, newWorld, generator);

                return newWorld;
            }
            else
            {
                IGenerator generator = new Scroll(new List<IGenerator> { new Ground(scoreListener) });

                IWorld newWorld = new World(
                    id,
                    WorldType.normal,
                    generator,
                    new List<Point>
                    {
                        new Point()
                    });

                newWorld.Extend(0, 0, 0, Const.GRID * 16);

                return newWorld;
            }
        }

        public void TransistOver()
        {
            State = GameState.gameOver;
            new TransistScript().Bind(controllers, this);
        }

        public void TransistGameWon()
        {
            game.ActivePlayer.Win();
            State = GameState.transist;
            new TransistScript().Bind(controllers, this);
        }

        public void Initialize(GameMode mode, IListener<ISoundable> soundEffectListener)
        {
            State = GameState.onProgress;
            Mode = mode;
            switch (mode)
            {
                case GameMode.normal:
                    map = "Level1.json";
                    break;
                case GameMode.infinite:
                    map = "Infinite.json";
                    break;
                case GameMode.randomMap:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
            game.ActivePlayer.InitCharacter("Mario", LoadLevel("Main"), scoreListener, soundEffectListener);
            session.Add(game.ActivePlayer);
            Resume();
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

        private void UpdateState(int time)
        {
            if (State == GameState.gameOver)
            {
                stateTimer += time;
                if (stateTimer >= 2000)
                {
                    stateTimer = 0;
                    State = GameState.onProgress;
                    game.ActivePlayer.Reset("Mario", scoreListener, new SoundEffectListener());
                    new PlayingScript().Bind(controllers, game.ActivePlayer.Character);
                }
            }
        }
    }
}

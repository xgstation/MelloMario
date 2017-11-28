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

        public enum GameMode
        {
            Normal,
            Infinite,
            RandomMap
        }

        private readonly Game1 game;
        private Session session;
        private IEnumerable<IController> controllers;
        private IListener<IGameObject> scoreListener;

        public GameState State { get; private set; }

        public GameModel(Game1 game)
        {
            this.game = game;
        }

        public Matrix GetActiveViewMatrix
        {
            get
            {
                return ActivePlayer.Camera.GetViewMatrix(new Vector2(1f));
            }
        }

        //TODO: Change with multiplayer
        public IPlayer ActivePlayer { get; private set; }

        public bool IsPaused { get; }

        public void ToggleFullScreen()
        {
            game.ToggleFullScreen();
        }


        public void Initialize()
        {
            State = GameState.Start;
            session = new Session();
            //ActivePlayer = new Player(session);
            scoreListener = new ScoreListener(this, ActivePlayer);
            Database.Initialize(session);
            //new StartScript().Bind(controllers, this, ActivePlayer.Character);
        }

        public void Pause()
        {
            State = GameState.Pause;
            new PausedScript().Bind(controllers, this);
        }

        public void Resume()
        {
            State = GameState.OnProgress;
            new PlayingScript().Bind(controllers, ActivePlayer.Character);
        }

        public void Reset()
        {
            game.Reset();
            Resume();
        }

        public void Quit()
        {
            game.Exit();
        }

        public void Infinite()
        {
            //mapPath = "Content/Infinite.json";
       //     ActivePlayer.Initialize("Mario", LoadLevel("Main"), scoreListener, soundListener);
            session.Add(ActivePlayer);
            State = GameState.OnProgress;
            Resume();
        }

        public void Normal()
        {
            //mapPath = "Content/Level1.json";
           // ActivePlayer.Initialize("Mario", LoadLevel("Main"), scoreListener, soundListener);
            session.Add(ActivePlayer);
            State = GameState.OnProgress;
            Resume();
        }
       
        public void Update(int time)
        {
            UpdateController();
            Database.Update();
            if (State == GameState.Pause)
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
            State = GameState.Transist;
            new TransistScript().Bind(controllers, this);
        }

        public void TransistGameWon()
        {
            ActivePlayer.Win();
            State = GameState.Transist;
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

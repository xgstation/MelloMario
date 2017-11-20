using System.Collections.Generic;
using MelloMario.Containers;
using MelloMario.LevelGen;
using MelloMario.MarioObjects;
using MelloMario.MarioObjects.ProtectionStates;
using MelloMario.MiscObjects;
using MelloMario.Scripts;
using MelloMario.Sounds;
using MelloMario.UIObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace MelloMario.Theming
{
    internal class GameModel : IGameModel
    {
        private readonly IPlayer activePlayer; // TODO: for singleplayer only
        private readonly Game1 game;
        private readonly IListener listener;
        private readonly GameSession session;
        private ICamera activeCamera;
        private IEnumerable<IController> controllers;
        private InfiniteGenerator infiniteGenerator;
        private bool isPaused;
        private string mapPath = "Content/Level1.json";

        //TODO: temporary public
        //note: we will have an extra class called Player which contains these information
        public IObject Splash;

        private int splashElapsed; // TODO: for sprint 4, refactor later

        public GameModel(Game1 game)
        {
            this.game = game;
            session = new GameSession();
            activePlayer = new Player(session);
            listener = new Listener(this, activePlayer);
            GameDatabase.Initialize(session);
        }

        public Matrix GetActiveViewMatrix
        {
            get
            {
                return activeCamera.GetViewMatrix(new Vector2(1f));
            }
        }

        public void ToggleFullScreen()
        {
            game.ToggleFullScreen();
        }

        public void Pause()
        {
            isPaused = true;

            new PausedScript().Bind(controllers, this, activePlayer.Character);
            MediaPlayer.Pause();
            splashElapsed = -1;
        }

        public void Init()
        {
            activeCamera = new Camera(game.GraphicsDevice.Viewport);

            activePlayer.Init("Mario", LoadLevel("Main"), listener, activeCamera);
            isPaused = true;
            new PlayingScript().Bind(controllers, this, activePlayer.Character);

            Splash = new GameStart(activePlayer); // TODO: move these constructors to the factory
            new StartScript().Bind(controllers, this, activePlayer.Character);
            splashElapsed = -1;
        }

        public void Resume()
        {
            isPaused = false;
            MediaPlayer.Resume();
            new PlayingScript().Bind(controllers, this, activePlayer.Character);

            Splash = new HUD(activePlayer);
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

        public void ToggleMute()
        {
            SoundController.ToggleMute();
        }

        public void Infinite()
        {
            mapPath = "Content/Infinite.json";
            splashElapsed = 0;
            activePlayer.Init("Mario", LoadLevel("Main"), listener, activeCamera);
            isPaused = false;
            Resume();
        }

        public void Normal()
        {
            mapPath = "Content/Level1.json";
            activePlayer.Init("Mario", LoadLevel("Main"), listener, activeCamera);
            splashElapsed = 0;
            isPaused = false;
            Resume();
        }

        public void Update(int time)
        {
            UpdateController();
            if (isPaused)
            {
                if (splashElapsed < 0)
                {
                    return;
                }
                splashElapsed += time;
                if (splashElapsed >= 1000 * 2)
                {
                    Resume();
                }
                return;
            }

            UpdateMusicScene(time);

            UpdateGameObjects(time);
            UpdateContainers();

            GameDatabase.Update(time);
            infiniteGenerator.Update(time);
        }

        public void Draw(int time, SpriteBatch spriteBatch)
        {
            IPlayer player = activePlayer;

            Rectangle drawRange = player.Character.Viewport;
            drawRange.Offset(-32, -32);
            drawRange.Inflate(64, 64);
            foreach (IGameObject obj in player.Character.CurrentWorld.ScanNearby(drawRange))
            {
                obj.Draw(isPaused ? 0 : time, spriteBatch);
            }

            Splash.Draw(time, spriteBatch);
        }

        public void LoadControllers(IEnumerable<IController> newControllers)
        {
            controllers = newControllers;
        }

        public IGameWorld LoadLevel(string id)
        {
            foreach (IGameWorld world in session.ScanWorlds())
            {
                if (world.Id == id)
                {
                    return world;
                }
            }

            // IGameWorld newWorld = new GameWorld(id, new Point(50, 20), new Point(1, 1), new List<Point>());

            LevelIOJson reader = new LevelIOJson(mapPath, listener);
            reader.SetModel(this);

            IGameWorld newWorld = reader.Load(id, session);
            activeCamera.Limit = newWorld.Boundary;
            infiniteGenerator = new InfiniteGenerator(newWorld, listener, activeCamera);
            return newWorld;
        }

        public void Transist()
        {
            activePlayer.Reset("Mario", listener);

            isPaused = true;
            new TransistScript().Bind(controllers, this, activePlayer.Character);

            Splash = new GameOver(activePlayer);
            splashElapsed = 0;
        }

        public void TransistGameWon()
        {
            activePlayer.Win();

            isPaused = true;
            MediaPlayer.Pause();
            new TransistScript().Bind(controllers, this, activePlayer.Character);

            Splash = new GameWon(activePlayer);
            splashElapsed = -1;
        }

        private void UpdateMusicScene(int time)
        {
            if (activePlayer.Character is MarioCharacter marioD && marioD.ProtectionState is Dead)
            {
                MediaPlayer.Play(SoundController.Normal);
            }
            else if (activePlayer.TimeRemain <= 90000)
            {
                SoundController.PlayMusic(SoundController.Songs.Hurry);
            }
            else if (activePlayer.Character.CurrentWorld.Id == "Main")
            {
                SoundController.PlayMusic(SoundController.Songs.Normal);
            }
            else
            {
                SoundController.PlayMusic(SoundController.Songs.BelowGround);
            }
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
                foreach (IGameObject obj in player.Character.CurrentWorld.ScanNearby(player.Character.Sensing))
                {
                    updating.Add(obj);
                }
            }

            updating.Add(Splash);

            foreach (IObject obj in updating)
            {
                obj.Update(time);
            }
        }

        private void UpdateContainers()
        {
            session.Update();
            foreach (IGameWorld world in session.ScanWorlds())
            {
                world.Update();
            }
        }
    }
}

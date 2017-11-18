using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.LevelGen;
using System;
using MelloMario.Scripts;
using MelloMario.Containers;
using MelloMario.MarioObjects;
using MelloMario.MiscObjects;
using MelloMario.UIObjects;
using MelloMario.Sounds;
using Microsoft.Xna.Framework;

namespace MelloMario.Theming
{
    class GameModel : IGameModel
    {
        private readonly Game1 game;
        private readonly GameSession session;
        private readonly IListener listener;
        private readonly IPlayer activePlayer; // TODO: for singleplayer only
        private ICamera activeCamera;
        private IEnumerable<IController> controllers;
        private bool isPaused;
        private int splashElapsed; // TODO: for sprint 4, refactor later
        //TODO: temporary public
        //note: we will have an extra class called Player which contains these information
        public IObject Splash;
        public Matrix GetActiveViewMatrix => activeCamera.GetViewMatrix(new Vector2(1f));

        public GameModel(Game1 game)
        {
            this.game = game;
            session = new GameSession();
            activePlayer = new Player(session);
            listener = new Listener(this, activePlayer);
            GameDatabase.Initialize(session);
        }

        public void LoadControllers(IEnumerable<IController> newControllers)
        {
            controllers = newControllers;
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

            LevelIOJson reader = new LevelIOJson("Content/Level1.json", listener);
            reader.SetModel(this);

            return reader.Load(id, session);
        }

        public void Init()
        {
            activeCamera = new Camera(game.GraphicsDevice.Viewport);
            activePlayer.Init("Mario", LoadLevel("Main"), listener, activeCamera);

            isPaused = true;
            new PlayingScript().Bind(controllers, this, activePlayer.Character);

            Splash = new GameStart(activePlayer); // TODO: move these constructors to the factory
            splashElapsed = 0;
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

        private void UpdateMusicScene(int time)
        {
            if (activePlayer.Character is MarioCharacter marioD &&
                marioD.ProtectionState is MarioObjects.ProtectionStates.Dead)
            {
                MediaPlayer.Play(SoundController.Normal);
            }
            if (activePlayer.Character is MarioCharacter mario &&
                mario.ProtectionState is MarioObjects.ProtectionStates.Starred)
            {
                MediaPlayer.Play(SoundController.Star);
            }
            else if (activePlayer.Lifes <= 1)
            {
                SoundController.PlayMusic(SoundController.Songs.GameOver);
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
                foreach (IObject obj in player.World.ScanNearby(player.Character.Sensing))
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
                if (splashElapsed >= 1000 * 3)
                {
                    Resume();
                }
                return;
            }

            UpdateMusicScene(time);

            UpdateGameObjects(time);
            UpdateContainers();

            GameDatabase.Update(time);
        }

        public void Draw(int time, SpriteBatch spriteBatch)
        {
            IPlayer player = activePlayer;

            foreach (IObject obj in player.World.ScanNearby(player.Character.Viewport))
            {
                if (isPaused)
                {
                    obj.Draw(0, spriteBatch);
                }
                else
                {
                    obj.Draw(time, spriteBatch);
                }
            }

            Splash.Draw(time, spriteBatch);
        }
    }
}

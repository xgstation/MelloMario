using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using MelloMario.LevelGen;
using System;
using MelloMario.Scripts;
using MelloMario.Containers;
using MelloMario.SplashObjects;
using MelloMario.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace MelloMario.Theming
{
    class GameModel : IGameModel
    {
        private Game1 game;
        private GameSession session;
        private IEnumerable<IController> controllers;
        private bool isPaused;
        private Listener listener;
        private int splashElapsed; // TODO: for sprint 4, refactor later
        //TODO: temporary public
        //note: we will have an extra class called Player which contains these information
        public int Coins;
        public int Score;
        public int Lives;
        public int Time;
        public IGameObject splash;

        // for singleplayer game
        private IPlayer GetActivePlayer()
        {
            foreach (IPlayer player in session.ScanPlayers())
            {
                // take only one
                return player;
            }

            return null; // error!
        }

        public GameModel(Game1 game)
        {
            this.game = game;
            session = new GameSession();
            listener = new Listener(this);
            GameDatabase.Initialize(session);
            Score = 0;
            Coins = 0;
            Lives = 3;
            Time = GameConst.LEVEL_TIME * 1000;
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

            new PausedScript().Bind(controllers, this, GetActivePlayer().Character);

            splashElapsed = -1;
        }

        public void Transist()
        {
            isPaused = true;

            new TransistScript().Bind(controllers, this, GetActivePlayer().Character);

            splash = new GameOver(this);
            splashElapsed = 0;
        }

        public void Resume()
        {
            isPaused = false;

            new PlayingScript().Bind(controllers, this, GetActivePlayer().Character);

            splash = new HUD(this);
        }

        public void Init()
        {
            isPaused = true;

            new PlayingScript().Bind(controllers, this, GetActivePlayer().Character);

            splash = new StartScreen(this);
            splashElapsed = 0;
        }

        public IGameWorld LoadLevel(string id, bool init = false)
        {

            foreach (IGameWorld world in session.ScanWorlds())
            {
                if (world.Id == id)
                {
                    return world;
                }
            }

            LevelIOJson reader = new LevelIOJson("Content/Level1.json", game.GraphicsDevice, listener);
            reader.SetModel(this);
            Tuple<IGameWorld, IPlayer> pair = reader.Load(id, session);

            if (!init && pair.Item2 != null)
            {
                session.Remove(pair.Item2);
            }
            session.Update();
            if (id == "Main") // TODO: load music name from level file
            {
                MediaPlayer.Stop();
                SoundController.PlayMusic(SoundController.Songs.normal);
            }
            else
            {
                MediaPlayer.Stop();
                SoundController.PlayMusic(SoundController.Songs.belowGround);
            }

            return pair.Item1;
        }
        
        public void SwitchMusic(int time)
        {
            if (time < 90000 && SoundController.CurrentSong != SoundController.Songs.hurry)
            {
                MediaPlayer.Stop();
                SoundController.PlayMusic(SoundController.Songs.hurry);
            }
            // TODO: Songs.gameOver should be triggered by gameover event
            if (time == 0 || Lives < 1)
            {
                MediaPlayer.Stop();
                SoundController.PlayMusic(SoundController.Songs.gameOver);
            }
        }

        public void Reset()
        {
            // TODO: "forced" version of LoadLevel()
            LoadLevel("Main", true);
            Resume();
        }

        public void Quit()
        {
            game.Exit();
        }

        public void Mute()
        {
            if (MediaPlayer.Volume > 0)
            {
                MediaPlayer.Volume = 0;
                SoundEffect.MasterVolume = 0;
            }
            else
            {
                MediaPlayer.Volume = 100;
                SoundEffect.MasterVolume = 1.0f;
            }
        }

        public void Update(int time)
        {
            // TODO: clean this part
            // TODO: use const

            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            if (isPaused)
            {
                if (splashElapsed >= 0)
                {
                    splashElapsed += time;
                    if (splashElapsed >= 1000 * 3)
                    {
                        Resume();
                    }
                }
            }
            else
            {
                // reserved for multiplayer
                ISet<IGameObject> updating = new HashSet<IGameObject>();

                foreach (IPlayer player in session.ScanPlayers())
                {
                    player.World.Update();
                    foreach (IGameObject obj in player.World.ScanNearby(player.Character.Sensing))
                    {
                        updating.Add(obj);
                    }
                }

                updating.Add(splash);

                foreach (IGameObject obj in updating)
                {
                    obj.Update(time);
                }

                // TODO: move to correct place
                Time -= time;
                SwitchMusic(Time);
            }
        }

        public void Draw(int time)
        {
            IPlayer player = GetActivePlayer();

            foreach (IGameObject obj in player.World.ScanNearby(player.Character.Viewport))
            {
                if (isPaused)
                {
                    obj.Draw(0, player.Character.Viewport);
                }
                else
                {
                    obj.Draw(time, player.Character.Viewport);
                }
            }

            splash.Draw(time, new Rectangle(new Point(), player.Character.Viewport.Size));
        }
    }
}

using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using MelloMario.LevelGen;
using System;
using MelloMario.Containers;
using MelloMario.Scripts;
using MelloMario.MiscObjects;
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
        //TODO: temporary public
        //note: we will have an extra class called Player which contains these information
        public int Coins;
        public int Score;
        public int Lives;
        public int Time;
        public IGameObject hud;

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
            if (!worldSwitched)
            {
                SoundController.PlayMusic(SoundController.Songs.normal);
            }

            Score = 0;
            Coins = 0;
            Lives = 3;
            Time = GameConst.LEVEL_TIME * 1000;
            hud = new HUD(this);

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
        }

        public void Resume()
        {
            isPaused = false;

            new PlayingScript().Bind(controllers, this, GetActivePlayer().Character);
        }

        private bool worldSwitched = false;
        public IGameWorld LoadLevel(string id, bool init = false)
        {

            foreach (IGameWorld world in session.ScanWorlds())
            {
                if (world.Id == id)
                {
                    return world;
                }
            }

            LevelIOJson reader = new LevelIOJson("Content/ExampleLevel.json", game.GraphicsDevice, listener);
            reader.SetModel(this);
            Tuple<IGameWorld, IPlayer> pair = reader.Load(id, session);


            if (!init && pair.Item2 != null)
            {
                session.Remove(pair.Item2);
            }
            session.Update();
            if (!worldSwitched)
            {
                MediaPlayer.Stop();
                SoundController.PlayMusic(SoundController.Songs.normal);
                worldSwitched = true;
            }
            else
            {
                MediaPlayer.Stop();
                SoundController.PlayMusic(SoundController.Songs.belowGround);
                worldSwitched = false;
            }
            Console.WriteLine(worldSwitched);
            return pair.Item1;
        }

        public void Init()
        {
            Resume();
        }

        // Method switches to "hurry" music when there are 90 seconds remaining
        public void switchMusic(int time)
        {
            if (time < 90000 && SoundController.CurrentSong != SoundController.Songs.hurry)
            {
                MediaPlayer.Stop();
                SoundController.PlayMusic(SoundController.Songs.hurry);
            }
            if (time == 0 || Lives < 1)
            {
                MediaPlayer.Stop();
                SoundController.PlayMusic(SoundController.Songs.gameOver);
            }
        }

        public void Reset()
        {
            // TODO: "forced" version of LoadLevel()
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
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            if (!isPaused)
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

                updating.Add(hud);

                foreach (IGameObject obj in updating)
                {
                    obj.Update(time);
                }

                // TODO: move to correct place
                Time -= time;
                switchMusic(Time);
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

            hud.Draw(time, new Rectangle(new Point(), player.Character.Viewport.Size));
        }
    }
}

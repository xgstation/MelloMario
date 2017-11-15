using System.Collections.Generic;
using MelloMario.LevelGen;
using System;
using MelloMario.Scripts;
using MelloMario.Containers;
using MelloMario.MarioObjects;
using MelloMario.UIObjects;
using MelloMario.Sounds;
using Microsoft.Xna.Framework;
#if PARALLEL
using System.Threading.Tasks;
#endif

namespace MelloMario.Theming
{
    class GameModel : IGameModel
    {
        private readonly Game1 game;
        private readonly GameSession session;
        private IEnumerable<IController> controllers;
        private bool isPaused;
        private readonly Listener listener;
        private int splashElapsed; // TODO: for sprint 4, refactor later
        //TODO: temporary public
        //note: we will have an extra class called Player which contains these information
        public IGameObject Splash;
        public SoundController.Songs ThemeMusic { get; private set; }
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
#if PARALLEL
        public delegate void performUpdate(int time);
        private performUpdate[] updateHandler;
#endif
        public GameModel(Game1 game)
        {
            this.game = game;
            session = new GameSession();
            listener = new Listener(this);
            ThemeMusic = SoundController.Songs.Idle;
            GameDatabase.Initialize(session);
            SoundController.Initialize(this);
#if PARALLEL
            updateHandler = new performUpdate[] { UpdateMusicScene, UpdateGameObjects, SoundController.Update, GameDatabase.Update };
#endif
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

            Splash = new GameOver(this);
            GameDatabase.TimeRemain = GameConst.LEVEL_TIME * 1000;
            splashElapsed = 0;

            GetActivePlayer().Reset();
        }

        public void TransistGameWon()
        {
            isPaused = true;

            new TransistScript().Bind(controllers, this, GetActivePlayer().Character);

            Splash = new GameWon(this);
            splashElapsed = -1;
        }

        public void Resume()
        {
            isPaused = false;

            new PlayingScript().Bind(controllers, this, GetActivePlayer().Character);

            Splash = new HUD(this);

        }

        public void Initialize()
        {
            isPaused = true;

            new PlayingScript().Bind(controllers, this, GetActivePlayer().Character);

            Splash = new GameStart();
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
            ThemeMusic = id == "Main" ? SoundController.Songs.Normal : SoundController.Songs.BelowGround;

            return pair.Item1;
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
            if (GetActivePlayer() is PlayerMario mario &&
                mario.ProtectionState is MarioObjects.ProtectionStates.Starred)
            {
                ThemeMusic = SoundController.Songs.Star;
            }
            else if (GameDatabase.TimeRemain < 90000)
            {
                ThemeMusic = SoundController.Songs.Hurry;
            }
            else
            {
                ThemeMusic = SoundController.Songs.Normal;
            }
            if (GameDatabase.TimeRemain == 0 || GameDatabase.Lifes < 1)
            {
                ThemeMusic = SoundController.Songs.GameOver;
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
            ISet<IGameObject> updating = new HashSet<IGameObject>();

            foreach (IPlayer player in session.ScanPlayers())
            {
                player.World.Update();
                foreach (IGameObject obj in player.World.ScanNearby(player.Character.Sensing))
                {
                    updating.Add(obj);
                }
            }

            updating.Add(Splash);

            foreach (IGameObject obj in updating)
            {
                obj.Update(time);
            }
        }



        public void Update(int time)
        {
            // TODO: clean this part
            // TODO: use const
            UpdateController();
            if (isPaused)
            {
                if (splashElapsed < 0) return;
                splashElapsed += time;
                if (splashElapsed >= 1000 * 3)
                {
                    Resume();
                }
                return;
            }
#if PARALLEL
            Parallel.ForEach(updateHandler, update => update(time));
#else
            SoundController.Update(time);
            UpdateMusicScene(time);
            UpdateGameObjects(time);
            GameDatabase.Update(time);
#endif
        }

        public void Draw(int time)
        {
            IPlayer player = GetActivePlayer();
#if PARALLEL
            Parallel.ForEach(player.World.ScanNearby(player.Character.Viewport), o =>
            {
                if (isPaused)
                {
                    o.Draw(0, player.Character.Viewport);
                }
                else
                {
                    o.Draw(time, player.Character.Viewport);
                }
            });
#else
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
#endif
            Splash.Draw(time, new Rectangle(new Point(), player.Character.Viewport.Size));
        }
    }
}

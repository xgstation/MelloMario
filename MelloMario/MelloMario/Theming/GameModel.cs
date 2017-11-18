using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using MelloMario.LevelGen;
using System;
using MelloMario.Scripts;
using MelloMario.Containers;
using MelloMario.MarioObjects;
using MelloMario.UIObjects;
using MelloMario.Sounds;
using Microsoft.Xna.Framework;

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
        public IObject Splash;
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

        public GameModel(Game1 game)
        {
            this.game = game;
            session = new GameSession();
            listener = new Listener(this, new Player(session)); // TODO
            ThemeMusic = SoundController.Songs.Idle;
            GameDatabase.Initialize(session);
            SoundController.Initialize(this);
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
            MediaPlayer.Pause();
            splashElapsed = -1;
        }

        public IGameWorld LoadLevel(string id)
        {
            ThemeMusic = id == "Main" ? SoundController.Songs.Normal : SoundController.Songs.BelowGround;

            foreach (IGameWorld world in session.ScanWorlds())
            {
                if (world.Id == id)
                {
                    return world;
                }
            }

            // IGameWorld newWorld = new GameWorld(id, new Point(50, 20), new Point(1, 1), new List<Point>());

            LevelIOJson reader = new LevelIOJson("Content/Level1.json", game.GraphicsDevice, listener);
            reader.SetModel(this);

            return reader.Load(id, session);
        }

        public void Init()
        {
            isPaused = true;

            new PlayingScript().Bind(controllers, this, GetActivePlayer().Character);

            Splash = new GameStart(GetActivePlayer()); // TODO: move these constructors to the factory
            splashElapsed = 0;

            LoadLevel("Main");
        }

        public void Transist()
        {
            isPaused = true;

            new TransistScript().Bind(controllers, this, GetActivePlayer().Character);

            Splash = new GameOver(GetActivePlayer());
            splashElapsed = 0;

            GetActivePlayer().Reset(null); // TODO
        }

        public void TransistGameWon()
        {
            isPaused = true;

            new TransistScript().Bind(controllers, this, GetActivePlayer().Character);

            Splash = new GameWon(GetActivePlayer());
            splashElapsed = -1;
        }

        public void Resume()
        {
            isPaused = false;
            MediaPlayer.Resume();
            new PlayingScript().Bind(controllers, this, GetActivePlayer().Character);

            Splash = new HUD(GetActivePlayer());
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
            if (GetActivePlayer().Character is MarioCharacter marioD &&
                marioD.ProtectionState is MarioObjects.ProtectionStates.Dead)
            {
                MediaPlayer.Play(SoundController.Normal);
            }
            if (GetActivePlayer().Character is MarioCharacter mario &&
                mario.ProtectionState is MarioObjects.ProtectionStates.Starred)
            {
                ThemeMusic = SoundController.Songs.Star;
            }
            else if (GetActivePlayer().Lifes <= 1)
            {
                ThemeMusic = SoundController.Songs.GameOver;
            }
            else if (GetActivePlayer().TimeRemain <= 90000)
            {
                ThemeMusic = SoundController.Songs.Hurry;
            }
            else
            {
                ThemeMusic = SoundController.Songs.Normal;
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
                player.World.Update();
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

        public void Update(int time)
        {
            // TODO: clean this part
            // TODO: use const
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
            SoundController.Update();
            UpdateMusicScene(time);
            UpdateGameObjects(time);
            GameDatabase.Update(time);
        }

        public void Draw(int time)
        {
            IPlayer player = GetActivePlayer();

            foreach (IObject obj in player.World.ScanNearby(player.Character.Viewport))
            {
                if (isPaused)
                {
                    obj.Draw(0);
                }
                else
                {
                    obj.Draw(time);
                }
            }

            Splash.Draw(time);
        }
    }
}

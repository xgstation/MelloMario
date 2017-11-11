using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MelloMario.LevelGen;
using System;
using MelloMario.Containers;
using MelloMario.Scripts;
using MelloMario.Factories;
using MelloMario.Theming;

namespace MelloMario
{
    class GameModel : IGameModel
    {
         
        private Game1 game;
        private GameSession session;
        private IEnumerable<IController> controllers;
        private bool isPaused;
        private Listener listener;

        private GameHUD timer;
        //TODO: temporary public until the can move hud out of game1
        public int Coins;
        public int Score;
        public string WorldIndex;

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
            timer = new GameHUD(this,400);
            Score = 0;
            Coins = 0;
            WorldIndex = "1-1";
            this.game = game;
            session = new GameSession();
            listener = new Listener(this);
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

            // TODO: move this to map initialization
            GameObjectFactory.Instance.CreateGameObject("EndFlagTop", pair.Item1, new Point(10 * 32, 13 * 32));
            GameObjectFactory.Instance.CreateGameObject("EndFlag", pair.Item1, new Point(10 * 32, 14 * 32));

            if (!init && pair.Item2 != null)
            {
                session.Remove(pair.Item2);
            }

            return pair.Item1;
        }

        public void Init()
        {
            session.Update();//force flush
            Resume();
        }

        public void Reset()
        {
            // TODO: "forced" version of LoadLevel()
            session.Update();//force flush
            Resume();
        }

        public void Quit()
        {
            game.Exit();
        }

        public void Update(int time)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            timer.Update(time);
            if (!isPaused)
            {
                // reserved for multiplayer
                ISet<IGameObject> updating = new HashSet<IGameObject>();

                foreach (IPlayer player in session.ScanPlayers())
                {
                    foreach (IGameObject obj in player.World.ScanNearby(player.Sensing))
                    {
                        updating.Add(obj);
                    }
                }

                foreach (IGameObject obj in updating)
                {
                    obj.Update(time);
                }

                foreach (IGameWorld world in session.ScanWorlds())
                {
                    world.Update();
                }

                session.Update();
            }
            
        }

        public void Draw(int time)
        {
            timer.Draw(time);
            IPlayer player = GetActivePlayer();
       

            foreach (ZIndex zIndex in Enum.GetValues(typeof(ZIndex)))
            {
                foreach (IGameObject obj in player.World.ScanNearby(player.Viewport))
                {
                    if (isPaused)
                    {
                        obj.Draw(0, player.Viewport, zIndex);
                    }
                    else
                    {
                        obj.Draw(time, player.Viewport, zIndex);
                    }
                }
            }
        }
    }
}

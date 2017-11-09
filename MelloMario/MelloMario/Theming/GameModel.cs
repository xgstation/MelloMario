using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MelloMario.LevelGen;
using System;
using MelloMario.Containers;
using MelloMario.Scripts;
using MelloMario.Factories;

namespace MelloMario
{
    class GameModel : IGameModel
    {
        private Game1 game;
        private GameSession session;
        private IEnumerable<IController> controllers;
        private bool isPaused;

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

            new PausedScript().Bind(controllers, this, GetActivePlayer());
        }

        public void Resume()
        {
            isPaused = false;

            new PlayingScript().Bind(controllers, this, GetActivePlayer());
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

            LevelIOJson reader = new LevelIOJson("Content/ExampleLevel.json", game.GraphicsDevice);
            reader.SetModel(this);
            Tuple<IGameWorld, IPlayer> pair = reader.Load(id, session);

            // TODO: move this to map initialization
            GameObjectFactory.Instance.CreateGameObject("EndFlagTop", pair.Item1, new Point(10 * 32, 13 * 32));
            GameObjectFactory.Instance.CreateGameObject("EndFlag", pair.Item1, new Point(10 * 32, 14 * 32));

            if (!init)
            {
                session.Remove(pair.Item2);
            }

            return pair.Item1;
        }

        public void Reset()
        {
            // TODO
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


            if (!isPaused)
            {
                // reserved for multiplayer
                ISet<IGameObject> updating = new HashSet<IGameObject>();

                foreach (IPlayer player in session.ScanPlayers())
                {
                    foreach (IGameObject obj in player.CurrentWorld.ScanNearby(player.Sensing))
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
            IPlayer player = GetActivePlayer();

            foreach (ZIndex zIndex in Enum.GetValues(typeof(ZIndex)))
            {
                foreach (IGameObject obj in player.CurrentWorld.ScanNearby(player.Viewport))
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

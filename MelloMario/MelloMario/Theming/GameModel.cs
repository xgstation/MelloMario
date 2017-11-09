using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MelloMario.LevelGen;
using System;
using MelloMario.Containers;
using MelloMario.Scripts;
using MelloMario.Theming;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    class GameModel : IGameModel
    {
        private Game1 game;
        private GameSession session;
        private IEnumerable<IController> controllers;
        private IPlayer player;
        private bool isPaused;

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

            new PausedScript().Bind(controllers, this, player);
        }

        public void Resume()
        {
            isPaused = false;

            new PlayingScript().Bind(controllers, this, player);
        }

        public void SwitchWorld(string id)
        {
            foreach (IGameWorld world in session.ScanWorlds())
            {
                if (world.Id == id)
                {
                    player.Spawn(world);
                    return;
                }
            }

            LevelIOJson reader = new LevelIOJson("Content/ExampleLevel.json", game.GraphicsDevice);
            reader.SetModel(this);
            Tuple<IGameWorld, IPlayer> pair = reader.Load(id);

            session.Move(player);
            player.Spawn(pair.Item1);
        }

        public void LoadLevel(string id)
        {
            LevelIOJson reader = new LevelIOJson("Content/ExampleLevel.json", game.GraphicsDevice);
            reader.SetModel(this);
            Tuple<IGameWorld, IPlayer> pair = reader.Load(id);

            // TODO: move this to map initialization
            pair.Item1.Add(Factories.GameObjectFactory.Instance.CreateGameObject("EndFlagTop", pair.Item1, new Point(10 * 32, 13 * 32)));
            pair.Item1.Add(Factories.GameObjectFactory.Instance.CreateGameObject("EndFlag", pair.Item1, new Point(10 * 32, 14 * 32)));

            player = pair.Item2;
            session.Add(player);

            Resume();
        }

        public void Reset()
        {
            GameDatabase.Clear();
            session.Remove(player);

            LoadLevel(player.CurrentWorld.Id);
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
                session.Update();

                // reserved for multiplayer
                ISet<IGameObject> updating = new HashSet<IGameObject>();

                foreach (IGameObject obj in player.CurrentWorld.ScanNearby(player.Sensing))
                {
                    updating.Add(obj);
                }

                foreach (IGameObject obj in updating)
                {
                    obj.Update(time);
                }

                player.CurrentWorld.Update();
            }
        }

        public void Draw(int time)
        {
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

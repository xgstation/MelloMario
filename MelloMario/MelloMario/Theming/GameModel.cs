using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MelloMario.LevelGen;
using System;
using MelloMario.Scripts;

namespace MelloMario
{
    class GameModel : IGameModel
    {
        private IDictionary<string, IGameWorld> worlds;
        private IGameWorld currentWorld;
        private string currentWorldIndex;

        private IEnumerable<IController> controllers;
        private IGameWorld world;
        private IPlayer player;
        private LevelIOJson reader;
        private bool isPaused;
        private Game1 game;

        public GameModel(Game1 game, LevelIOJson reader)
        {
            this.game = game;
            this.reader = reader;
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
            isPaused = !isPaused;

            if (isPaused)
            {
                new PausedScript().Bind(controllers, this, player);
            }
            else
            {
                new PlayingScript().Bind(controllers, this, player);
            }
        }

        public void Reset()
        {
            reader.SetModel(this);
            Tuple<IGameWorld, IPlayer> pair = reader.Load("Main");
            world = pair.Item1;
            player = pair.Item2;

            isPaused = false;
            new PlayingScript().Bind(controllers, this, player);
        }

        public void Quit()
        {
            game.Exit();
        }

        public void Update(GameTime time)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            if (!isPaused)
            {
                foreach (IGameObject obj in world.ScanAll())
                {
                    obj.Update(time);
                }

                world.Update();
            }
        }

        public void Draw(GameTime time)
        {
            if (isPaused)
            {
                // no animation on pause
                time.ElapsedGameTime = new TimeSpan();
            }

            foreach (ZIndex zIndex in Enum.GetValues(typeof(ZIndex)))
            {
                foreach (IGameObject obj in world.ScanNearby(world.Boundary))
                {
                    obj.Draw(time, player.Viewport, zIndex);
                }
            }
        }

        public void SwitchWorld(string index)
        {
            Point newLoc = new Point(1, 1);

            if (worlds.ContainsKey(index))
            {
                currentWorld = worlds[index];
            }
            else
            {
                Tuple<IGameWorld, IPlayer> pair = reader.Load(index);
                worlds.Add(currentWorldIndex, currentWorld);
                currentWorld = pair.Item1;
            }

            player.Spawn(currentWorld);
        }
    }
}

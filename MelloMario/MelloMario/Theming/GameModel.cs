using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private IGameControl control;
        private LevelIOJson reader;
        private bool isPaused;
        private Game1 game;

        // TODO: remove this
        public IGameControl Control { get { return control; } }

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
                new PausedScript().Bind(controllers, this, control);
            }
            else
            {
                new PlayingScript().Bind(controllers, this, control);
            }
        }

        public void Reset()
        {
            reader.SetModel(this);
            Tuple<IGameWorld, IGameControl> pair = reader.Load("Main");
            world = pair.Item1;
            control = pair.Item2;

            isPaused = false;
            new PlayingScript().Bind(controllers, this, control);
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
                foreach (IGameObject obj in world.ScanObjects())
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
                foreach (IGameObject obj in world.ScanObjects())
                {
                    obj.Draw(time, control.Viewport, zIndex);
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
                var pair = reader.Load(index);
                worlds.Add(currentWorldIndex, currentWorld);
                currentWorld = pair.Item1;
            }
            control.Spawn(currentWorld);
        }
    }
}

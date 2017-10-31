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
        private IEnumerable<IController> controllers;
        private IGameWorld world;
        private IGameCharacter character;
        private LevelIOJson reader;
        private bool isPaused;
        private Game1 game;

        // TODO: remove this
        internal IGameCharacter Character { get { return character != null ? character : null; } }

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
                new PausedScript().Bind(controllers, character, this);
            }
            else
            {
                new PlayingScript().Bind(controllers, character, this);
            }
        }

        public void Reset()
        {
            reader.SetModel(this);
            Tuple<IGameWorld, IGameCharacter> pair = reader.Load("Main");
            world = pair.Item1;
            character = pair.Item2;

            isPaused = false;
            new PlayingScript().Bind(controllers, character, this);
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

        public void Draw(GameTime time, ZIndex zIndex)
        {
            if (isPaused)
            {
                // no animation on pause
                time.ElapsedGameTime = new TimeSpan();
            }

            foreach (IGameObject obj in world.ScanObjects())
            {
                obj.Draw(time, character.Viewport, zIndex);
            }
        }
    }
}

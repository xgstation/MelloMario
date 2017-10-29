using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.LevelGen;
using System;

namespace MelloMario
{
    class GameModel : IGameModel
    {
        private IEnumerable<IController> controllers;
        private IGameWorld world;
        private IGameCharacter character;
        private GameScript script;
        private LevelIOJson reader;
        private bool isPaused;

        public bool IsPaused
        {
            get
            {
                return isPaused;
            }
        }

        public GameModel(GameScript script, LevelIOJson reader)
        {
            this.script = script;
            this.reader = reader;
        }

        public void LoadControllers(IEnumerable<IController> newControllers)
        {
            controllers = newControllers;
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
            foreach (IGameObject obj in world.ScanObjects())
            {
                obj.Draw(time, character.Viewport, zIndex);
            }
        }

        public void Pause()
        {
            isPaused = !isPaused;
        }

        public void Reset()
        {
            Tuple<IGameWorld, IGameCharacter> pair = reader.Load("Main");
            world = pair.Item1;
            character = pair.Item2;

            script.Bind(controllers, character, this);
        }

        public void Quit()
        {
            // TODO
        }
    }
}

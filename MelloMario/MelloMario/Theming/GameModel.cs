using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.LevelGen;
using System;

namespace MelloMario
{
    class GameModel
    {
        private IEnumerable<IController> controllers;
        private IGameWorld world;
        private IGameCharacter character;

        public GameModel()
        {
        }

        public void LoadControllers(IEnumerable<IController> newControllers)
        {
            controllers = newControllers;
        }

        public void Bind(GameScript script)
        {
            script.Bind(controllers, character, this);
        }

        public void LoadEntities(LevelReader reader)
        {
            Tuple<IGameWorld, IGameCharacter> pair = reader.LoadObjects();
            world = pair.Item1;
            character = pair.Item2;
        }

        public void Update(GameTime time)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            foreach (IGameObject obj in world.ScanObjects())
            {
                obj.Update(time);
            }

            world.Update();
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
            // TODO
        }

        public void Reset()
        {
            // TODO
        }

        public void Quit()
        {
            // TODO
        }
    }
}

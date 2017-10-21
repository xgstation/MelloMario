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

            ICollection<IGameObject> movedObjects = new List<IGameObject>();
            foreach (IGameObject obj in world.ScanObjects())
            {
                Rectangle oldBoundary = obj.Boundary;

                obj.Update(time);

                if (obj.Boundary != oldBoundary)
                {
                    movedObjects.Add(obj);
                }
            }

            foreach (IGameObject obj in movedObjects)
            {
                world.RemoveObject(obj);
                world.AddObject(obj);
            }
        }

        internal void Draw(GameTime time, ZIndex zIndex)
        {
            foreach (IGameObject obj in world.ScanObjects())
            {
                obj.Draw(time, zIndex);
            }
        }

        public void Quit()
        {
            // Add quit command
        }
    }
}

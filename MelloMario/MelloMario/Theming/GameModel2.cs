using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.LevelGen;
using System;

namespace MelloMario
{
    class GameModel2
    {
        private LevelIOJson levelLoader;
        private LevelIOJson levelSaver;

        private IEnumerable<IController> controllers;
        private IEnumerable<IGameWorld> worlds;
        private IEnumerable<IGameCharacter> characters;

        private IGameWorld world;
        private IGameCharacter character;

        public GameModel2()
        {
        }

        public void LoadControllers(IEnumerable<IController> newControllers)
        {
            controllers = newControllers;
        }

        public void Bind(GameScript script)
        {
            //script.Bind(controllers, character);
        }

        public void LoadEntities(LevelIOJson loader)
        {
            var pair = loader.Load();
            worlds = pair.Item1;
            characters = pair.Item2;
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
    }
}

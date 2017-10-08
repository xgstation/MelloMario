using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.MarioObjects;
using MelloMario.BlockObjects;
using MelloMario.EnemyObjects;
using MelloMario.ItemObjects;
using MelloMario.LevelGen;

namespace MelloMario
{
    class GameModel
    {
        private IEnumerable<IController> controllers;

        private IGameObjectManager objectManager;
        // this is a "pointer" this mario also exists in all objects.
        private Mario mario;

        public GameModel()
        {
        }

        public void LoadControllers(IEnumerable<IController> newControllers)
        {
            controllers = newControllers;
        }

        public void Bind(GameScript script)
        {
            script.Bind(controllers, mario);
        }

        public void LoadEntities(LevelReader reader)
        {
            objectManager = reader.LoadObjects();
            mario = reader.BindMario();
        }

        public void Update(GameTime time)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            ICollection<IGameObject> movedObjects = new List<IGameObject>();
            foreach (IGameObject obj in objectManager.ScanObjects())
            {
                Rectangle oldBoundary = obj.Boundary;

                obj.Update(time, objectManager.ScanNearbyObjects(obj));

                if (obj.Boundary != oldBoundary)
                {
                    movedObjects.Add(obj);
                }
            }

            foreach (IGameObject obj in movedObjects)
            {
                objectManager.RemoveObject(obj);
                objectManager.AddObject(obj);
            }
        }

        internal void Draw(GameTime time, SpriteBatch spriteBatch)
        {
            mario.Draw(time, spriteBatch);

            foreach (IGameObject obj in objectManager.ScanObjects())
            {
                obj.Draw(time, spriteBatch);
            }
        }
    }
}

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
        private IList<IController> controllers;
        private IGameObject[,] stationaryObjects;
        private List<IGameObject> dynamicObjects;
        // TODO: Do we need another abstraction layer for mario's actions?
        private Mario mario;

        public GameModel()
        {
            dynamicObjects = new List<IGameObject>();
        }

        public void LoadControllers(IList<IController> controllers)
        {
            this.controllers = controllers;
        }

        public void Bind(GameScript script)
        {
            script.Bind(controllers, mario, stationaryObjects, dynamicObjects);
        }

        public void LoadEntities(LevelReader reader)
        {
            stationaryObjects = reader.LoadStatic();
            //these need to be called after loadstatic, this is a little bit messy
            //will probably change in the future
            dynamicObjects = reader.LoadDynamic();
            mario = reader.LoadMario();
        }

        public void Update(GameTime time)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            mario.Update(time, new List<IGameObject>());

            foreach (IGameObject gameObject in dynamicObjects)
            {
                gameObject.Update(time, new List<IGameObject>());
            }

            for (int i = 0; i < stationaryObjects.GetLength(0); ++i)
            {
                for (int j = 0; j < stationaryObjects.GetLength(1); ++j)
                {
                    if (stationaryObjects[i, j] != null)
                    {
                        // note: the list will contain all objects that are possible to collide with objects[i, j]
                        // BaseGameObject.OnCollision may be triggered multiple times (actually, it is a status instead of an event)
                        stationaryObjects[i, j].Update(time, new List<IGameObject>());
                    }
                }
            }

        }

        internal void Draw(GameTime time, SpriteBatch spriteBatch)
        {
            mario.Draw(time, spriteBatch);

            foreach (IGameObject gameObject in dynamicObjects)
            {
                gameObject.Draw(time, spriteBatch);
            }

            for (int i = 0; i < stationaryObjects.GetLength(0); ++i)
            {
                for (int j = 0; j < stationaryObjects.GetLength(1); ++j)
                {
                    if (stationaryObjects[i, j] != null)
                    {
                        stationaryObjects[i, j].Draw(time, spriteBatch);
                    }
                }
            }
        }
    }
}
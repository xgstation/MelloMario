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
        private List<IGameObject>[,] allObjects;
        // this is a "pointer" this mario also exists in all objects.
        private Mario mario;

        public GameModel()
        {
        }

        public void LoadControllers(IList<IController> controllers)
        {
            this.controllers = controllers;
        }

        public void Bind(GameScript script)
        {
            script.Bind(controllers, mario, allObjects);
        }

        public void LoadEntities(LevelReader reader)
        {
            allObjects = reader.LoadObjects();
            mario = reader.BindMario();
        }

        public void Update(GameTime time)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            for (int i = 0; i < allObjects.GetLength(0); ++i)
            {
                for (int j = 0; j < allObjects.GetLength(1); ++j)
                {
                    foreach(IGameObject obj in allObjects[i,j])
                    {
                        obj.Update(time, new List<IGameObject>());
                    }
                }
            }

        }

        internal void Draw(GameTime time, SpriteBatch spriteBatch)
        {
            mario.Draw(time, spriteBatch);

            for (int i = 0; i < allObjects.GetLength(0); ++i)
            {
                for (int j = 0; j < allObjects.GetLength(1); ++j)
                {
                    foreach (IGameObject obj in allObjects[i,j])
                    {
                        obj.Draw(time, spriteBatch);
                    }
                }
            }
        }
    }
}
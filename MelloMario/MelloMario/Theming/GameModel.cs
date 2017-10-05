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
        private List<IController> controllers;
        private IGameObject[,] stationaryObjects;
        private List<IGameObject> dynamicObjects;
        // TODO: Do we need another abstraction layer for mario's actions?
        private Mario mario;

        public GameModel()
        {
            dynamicObjects = new List<IGameObject>();
        }

        public void LoadControllers(List<IController> controllers)
        {
            this.controllers = controllers;
        }

        public void Bind(GameScript script)
        {
            script.Bind(controllers, mario, stationaryObjects);
        }

        public void LoadEntities(LevelReader reader)
        {
            stationaryObjects = reader.LoadStatic();
            dynamicObjects = reader.LoadDynamic();

            //hard coded items and blocks for testing purposes
            /*
            stationaryObjects = new IGameObject[22, 22];
            mario = new Mario(new Point(100, 100));
            stationaryObjects[0, 0] = mario;
            Brick blockHidden = new Brick(new Point(300, 200));
            blockHidden.State.Hide();
            stationaryObjects[1, 0] = (blockHidden);
            stationaryObjects[2, 0] = (new Brick(new Point(100, 200)));
            stationaryObjects[3, 0] = (new Question(new Point(150, 200)));
            stationaryObjects[4, 0] = (new Stair(new Point(200, 200)));
            stationaryObjects[0, 1] = (new Floor(new Point(250, 200)));
            stationaryObjects[2, 2] = (new Pipeline(new Point(350, 168)));
            stationaryObjects[1, 1] = (new Coin(new Point(50, 50)));
            dynamicObjects.Add(new Goomba(new Point(100, 300)));
            dynamicObjects.Add(new GreenKoopa(new Point(100, 400)));
            dynamicObjects.Add(new RedKoopa(new Point(200, 400)));
            dynamicObjects.Add(new FireFlower(new Point(100, 50)));
            dynamicObjects.Add(new OneUpMushroom(new Point(200, 50)));
            dynamicObjects.Add(new SuperMushroom(new Point(250, 50)));
            dynamicObjects.Add(new Star(new Point(300, 50)));
            */
        }

        public void Update(GameTime time)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            foreach(IGameObject gameObject in dynamicObjects)
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
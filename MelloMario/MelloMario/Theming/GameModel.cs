using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.MarioObjects;
using MelloMario.BlockObjects;
using MelloMario.EnemyObjects;
using MelloMario.ItemObjects;

namespace MelloMario
{
    class GameModel
    {
        private List<IController> controllers;
        private IGameObject[,] objects;
        // TODO: Do we need another abstraction layer for mario's actions?
        private Mario mario;

        public GameModel()
        {
        }

        public void LoadControllers(List<IController> controllers)
        {
            this.controllers = controllers;
        }

        public void Bind(GameScript script)
        {
            script.Bind(controllers, mario, objects);
        }

        public void LoadEntities(int w, int h)
        {
            objects = new IGameObject[w, h];
            mario = new Mario(new Point(100, 100));

            //all of these array cords are randomly picked ignore them
            objects[0, 0] = mario;
            //temporary hard coded blocks
            Brick blockHidden = new Brick(new Point(300, 200));
            blockHidden.State.Hide();
            objects[1, 0] = (blockHidden);
            objects[2, 0] = (new Brick(new Point(100, 200)));
            objects[3, 0] = (new Question(new Point(150, 200)));
            objects[4, 0] = (new Stair(new Point(200, 200)));
            objects[0, 1] = (new Floor(new Point(250, 200)));
            objects[0, 2] = (new Goomba(new Point(100, 300)));
            objects[0, 3] = (new GreenKoopa(new Point(100, 400)));
            objects[0, 4] = (new RedKoopa(new Point(200, 400)));
            objects[1, 1] = (new Coin(new Point(50, 50)));
            objects[1, 2] = (new FireFlower(new Point(100, 50)));
            objects[1, 3] = (new OneUpMushroom(new Point(200, 50)));
            objects[1, 4] = (new SuperMushroom(new Point(250, 50)));
            objects[2, 1] = (new Star(new Point(300, 50)));
            objects[2, 2] = (new Pipeline(new Point(350, 168)));

        }

        public void Update(GameTime time)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            for (int i = 0; i < objects.GetLength(0); ++i)
            {
                for (int j = 0; j < objects.GetLength(1); ++j)
                {
                    if (objects[i, j] != null)
                    {
                        // note: the list will contain all objects that are possible to collide with objects[i, j]
                        // BaseGameObject.OnCollision may be triggered multiple times (actually, it is a status instead of an event)
                        objects[i, j].Update(time, new List<IGameObject>());
                    }
                }
            }

        }

        internal void Draw(GameTime time, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < objects.GetLength(0); ++i)
            {
                for (int j = 0; j < objects.GetLength(1); ++j)
                {
                    if (objects[i, j] != null)
                    {
                        objects[i, j].Draw(time, spriteBatch);
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.MarioObjects;
using MelloMario.BlockObjects;
using MelloMario.EnemyObjects;
using MelloMario.ItemObjects;

namespace MelloMario
{
    public class GameModel
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
            mario = new Mario(new Vector2(100, 100));

            //all of these array cords are randomly picked ignore them
            objects[0, 0] = mario;
            //temporary hard coded blocks
            BrickBlock blockHidden = new BrickBlock(new Vector2(300, 200));
            blockHidden.State.ChangeToHidden();
            objects[1, 0] = (blockHidden);
            objects[2, 0] = (new BrickBlock(new Vector2(100, 200)));
            objects[3, 0] = (new QuestionBlock(new Vector2(150, 200)));
            objects[4, 0] = (new StairBlock(new Vector2(200, 200)));
            objects[0, 1] = (new FloorBlock(new Vector2(250, 200)));
            objects[0, 2] = (new Goomba(new Vector2(100, 300)));
            objects[0, 3] = (new GreenKoopa(new Vector2(100, 400)));
            objects[0, 4] = (new RedKoopa(new Vector2(200, 400)));
            objects[1, 1] = (new Coin(new Vector2(50, 50)));
            objects[1, 2] = (new FireFlower(new Vector2(100, 50)));
            objects[1, 3] = (new OneUpMushroom(new Vector2(200, 50)));
            objects[1, 4] = (new SuperMushroom(new Vector2(250, 50)));
            objects[2, 1] = (new Star(new Vector2(300, 50)));
            objects[2, 2] = (new PipelineBlock(new Vector2(350, 168)));

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
                        objects[i, j].Update(time);
                }
            }

        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < objects.GetLength(0); ++i)
            {
                for (int j = 0; j < objects.GetLength(1); ++j)
                {
                    if (objects[i, j] != null)
                        objects[i, j].Draw(spriteBatch);
                }
            }
        }
    }
}
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
        private List<IGameObject> objects;
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

        public void LoadEntities()
        {
            objects = new List<IGameObject>();
            mario = new Mario(new Vector2(100, 100));
            
            objects.Add(mario);
            //temporary hard coded blocks
            BrickBlock blockHidden = new BrickBlock(new Vector2(300, 200));
            blockHidden.State.ChangeToHidden();
            objects.Add(blockHidden);
            objects.Add(new BrickBlock(new Vector2(100, 200)));
            objects.Add(new QuestionBlock(new Vector2(150, 200), objects));
            objects.Add(new StairBlock(new Vector2(200, 200)));
            objects.Add(new FloorBlock(new Vector2(250, 200)));
            objects.Add(new Goomba(new Vector2(100, 300)));
            objects.Add(new GreenKoopa(new Vector2(100, 400)));
            objects.Add(new RedKoopa(new Vector2(200, 400)));
            objects.Add(new Coin(new Vector2(50, 50)));
            objects.Add(new FireFlower(new Vector2(100,50)));
            objects.Add(new OneUpMushroom(new Vector2(200, 50)));
            objects.Add(new SuperMushroom(new Vector2(250, 50)));
            objects.Add(new Star(new Vector2(300, 50)));
            objects.Add(new PipelineBlock(new Vector2(350, 168)));
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }
            for(int i = 0; i < objects.Count; ++i)
            {
                objects[i].Update(gameTime);
            }

        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            foreach (IGameObject gameObject in objects)
            {
                gameObject.Draw(spriteBatch);
            }
        }
    }
}
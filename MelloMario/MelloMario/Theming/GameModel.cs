using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.MarioObjects;
using MelloMario.BlockObjects;

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
            script.Bind(controllers, this, mario);
        }

        public void LoadEntities()
        {
            objects = new List<IGameObject>();
            mario = new Mario(new Vector2(100, 100));
            objects.Add(mario);
            //temporary hard coded blocks
            objects.Add(new BrickBlock(new Vector2(100, 200)));
            objects.Add(new QuestionBlock(new Vector2(150, 200)));
            objects.Add(new StairBlock(new Vector2(200, 200)));
            objects.Add(new FloorBlock(new Vector2(250, 200)));
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }
            foreach (IGameObject gameObject in objects)
            {
                gameObject.Update(gameTime);
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
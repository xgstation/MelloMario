using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.MarioObjects;

namespace MelloMario
{
    public class GameModel
    {
        private List<IController> controllers;
        private List<IGameObject> objects;
        // TODO: Do we need another abstraction layer for mario's actions?
        private Mario mario;

        internal List<IController> Controllers
        {
            get
            {
                return controllers;
            }
        }
        internal Mario Mario
        {
            get
            {
                return mario;
            }
        }

        public GameModel()
        {
            objects = new List<IGameObject>();
        }

        internal void Initialize(List<IController> controllers)
        {
            this.controllers = controllers;

        }

        internal void LoadEntities()
        {

            mario = new Mario(new Vector2(100, 100));

        }

        internal void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers)
            {
                controller.UpdateInput();
            }
            mario.Update(gameTime);

            foreach (IGameObject gameObject in objects)
            {
                gameObject.Update(gameTime);
            }

        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            mario.Draw(spriteBatch);

            foreach (IGameObject gameObject in objects)
            {
                gameObject.Draw(spriteBatch);
            }
        }
    }
}
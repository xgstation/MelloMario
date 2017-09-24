using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    public class GameModel
    {
        public List<Controller> controllers;
        private List<ISprite> sprites;
        // TODO: Do we need another abstraction layer for mario's actions?
        private Mario mario;

        internal Mario Mario
        {
            get
            {
                return mario;
            }
        }

        public GameModel()
        {
            sprites = new List<ISprite>();
            mario = new Mario(new Vector2(100, 00));
        }

        internal void Initialize(List<Controller> controllers)
        {
            this.controllers = controllers;

            
        }

        internal void update(GameTime gameTime)
        {
            foreach (Controller controller in controllers)
            {
                controller.UpdateInput();
            }
            mario.Update(gameTime);
            foreach (ISprite sprite in sprites)
            {
                sprite.Update(gameTime);
            }

        }

        internal void draw(SpriteBatch spriteBatch)
        {
            mario.Draw(spriteBatch);
            foreach (ISprite sprite in sprites)
            {
                sprite.Draw(spriteBatch, new Vector2(0,0));
            }
        }
    }
}
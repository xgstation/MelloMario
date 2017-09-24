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
        private Mario Mario;

        public GameModel()
        {
            sprites = new List<ISprite>();
            Mario = new Mario(new Vector2(100, 00));
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
            Mario.Update(gameTime);
            foreach (ISprite sprite in sprites)
            {
                sprite.Update(gameTime);
            }

        }

        internal void draw(SpriteBatch spriteBatch)
        {
            Mario.Draw(spriteBatch);
            foreach (ISprite sprite in sprites)
            {
                sprite.Draw(spriteBatch);
            }
        }
    }
}
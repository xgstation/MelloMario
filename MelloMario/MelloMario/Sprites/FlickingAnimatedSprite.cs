using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace MelloMario.Sprites
{
    class FlickingAnimatedSprite : AnimatedSprite
    {
        private Random random;

        protected override void OnFrame()
        {
            ChangeColor(new Color(random.Next(255), random.Next(255), random.Next(255)));
        }

        public FlickingAnimatedSprite(SpriteBatch spriteBatch, Texture2D texture, int columns, int rows, ZIndex activeZIndex = ZIndex.item, int interval = 100) : base(
            spriteBatch, texture, columns, rows, activeZIndex, interval
        )
        {
            random = new Random();
        }
    }
}

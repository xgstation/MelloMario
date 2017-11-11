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

        public FlickingAnimatedSprite(SpriteBatch spriteBatch, Texture2D texture, int interval, int columns, int rows, ZIndex zIndex = ZIndex.item) : base(
            spriteBatch, texture, interval, columns, rows, zIndex
        )
        {
            random = new Random();
        }
    }
}

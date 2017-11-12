using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using MelloMario.Theming;

namespace MelloMario.Sprites
{
    class FlickingAnimatedSprite : AnimatedSprite
    {
        private Random random;

        protected override void OnFrame()
        {
            ChangeColor(new Color(random.Next(255), random.Next(255), random.Next(255)));
        }

        public FlickingAnimatedSprite(SpriteBatch spriteBatch, Texture2D texture, int columns, int rows, int x = 0, int y = 0, int width = 2, int height = 2, int interval = GameConst.ANIMATION_INTERVAL, ZIndex zIndex = ZIndex.item) : base(
            spriteBatch, texture, columns, rows, x, y, width, height, interval, zIndex
        )
        {
            random = new Random();
        }
    }
}

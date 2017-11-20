using System;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Sprites
{
    internal class FlickingAnimatedSprite : AnimatedSprite
    {
        private readonly Random random;

        public FlickingAnimatedSprite(Texture2D texture, int columns, int rows, int x = 0, int y = 0, int width = 2, int height = 2, int interval = GameConst.ANIMATION_INTERVAL, ZIndex zIndex = ZIndex.Item) : base(texture, columns, rows, x, y, width, height, interval, zIndex)
        {
            random = new Random();
        }

        protected override void OnFrame()
        {
            ChangeColor(new Color(random.Next(255), random.Next(255), random.Next(255)));
        }
    }
}

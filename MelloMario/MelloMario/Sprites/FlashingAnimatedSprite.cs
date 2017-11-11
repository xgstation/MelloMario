using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace MelloMario.Sprites
{
    class FlashingAnimatedSprite : AnimatedSprite
    {
        protected override void OnFrame()
        {
            Toggle();
        }

        public FlashingAnimatedSprite(SpriteBatch spriteBatch, Texture2D texture, int columns, int rows, ZIndex zIndex = ZIndex.item, int interval = 100) : 
            base(spriteBatch, texture, columns, rows, zIndex, interval)
        {
        }
    }
}

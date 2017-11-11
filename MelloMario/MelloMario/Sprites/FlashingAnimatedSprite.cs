using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Sprites
{
    class FlashingAnimatedSprite : AnimatedSprite
    {
        protected override void OnFrame()
        {
            Toggle();
        }

        public FlashingAnimatedSprite(SpriteBatch spriteBatch, Texture2D texture, int interval, int columns, int rows, ZIndex zIndex = ZIndex.item) : base(
            spriteBatch, texture, interval, columns, rows, zIndex
        )
        {
        }
    }
}

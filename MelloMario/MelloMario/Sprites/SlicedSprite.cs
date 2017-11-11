using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Sprites
{
    class SlicedSprite : BaseTextureSprite
    {
        protected override void OnAnimate(int time)
        {
            // Do nothing
        }

        public SlicedSprite(SpriteBatch spriteBatch, Texture2D texture, int columns, int rows, int x, int y, int width, int height, ZIndex zIndex = ZIndex.item) : 
            base(spriteBatch, texture, new Point(texture.Width * x / columns, texture.Height * y / rows), new Point(texture.Width * width / columns, texture.Height * height / rows), zIndex)
        {
        }
    }
}

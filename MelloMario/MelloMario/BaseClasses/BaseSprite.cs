using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MelloMario.Sprites
{
    abstract class BaseSprite : ISprite
    {
        private Texture2D texture;
        private Point source;
        private Point size;
        private Color defaultColor;

        public BaseSprite(Texture2D newTexture, Point newSource, Point newSize)
        {
            this.texture = newTexture;
            this.source = newSource;
            this.size = newSize;
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destination)
        {
            spriteBatch.Draw(texture, destination, new Rectangle(source, size), defaultColor);
        }
    }
}

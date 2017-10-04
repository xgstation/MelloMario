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

        protected abstract void OnAnimate(GameTime time);

        public BaseSprite(Texture2D texture, Point source, Point size)
        {
            this.texture = texture;
            this.source = source;
            this.size = size;
        }

        public void Draw(GameTime time, SpriteBatch spriteBatch, Rectangle destination)
        {
            spriteBatch.Draw(texture, destination, new Rectangle(source, size), defaultColor);
        }
    }
}

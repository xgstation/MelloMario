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

        protected void ChangeSource(Point source)
        {
            this.source = source;
        }

        protected void ChangeSize(Point size)
        {
            this.size = size;
        }

        public Point PixelSize
        {
            get
            {
                return new Point(size.X, size.Y); // Do copy to prevent unintended change of value
            }
        }

        public BaseSprite(Texture2D texture, Point source, Point size)
        {
            this.texture = texture;
            this.source = source;
            this.size = size;
        }

        public void Draw(GameTime time, SpriteBatch spriteBatch, Rectangle destination)
        {
            OnAnimate(time);

            spriteBatch.Draw(texture, destination, new Rectangle(source, size), defaultColor);
        }
    }
}

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MelloMario.Sprites
{
    abstract class BaseSprite : ISprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Point source;
        private Point size;

        protected abstract void OnAnimate(GameTime time);

        protected void ChangeSource(Point newSource)
        {
            source = newSource;
        }

        public Point PixelSize
        {
            get
            {
                return new Point(size.X, size.Y); // Do copy to prevent unintended change of value
            }
        }

        public BaseSprite(SpriteBatch spriteBatch, Texture2D texture, Point source, Point size)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.source = source;
            this.size = size;
        }

        public void Draw(GameTime time, Rectangle destination)
        {
            OnAnimate(time);

            spriteBatch.Draw(texture, destination, new Rectangle(source, size), Color.White);
        }
    }
}

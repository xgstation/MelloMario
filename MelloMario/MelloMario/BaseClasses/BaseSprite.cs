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
        private ZIndex activeZIndex;

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

        public BaseSprite(SpriteBatch spriteBatch, Texture2D texture, Point source, Point size, ZIndex activeZIndex)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.source = source;
            this.size = size;
            this.activeZIndex = activeZIndex;
        }

        public void Draw(GameTime time, Rectangle destination, ZIndex zIndex)
        {
            if (activeZIndex == zIndex)
            {
                OnAnimate(time);

                spriteBatch.Draw(texture, destination, new Rectangle(source, size), Color.White);
            }
        }
    }
}

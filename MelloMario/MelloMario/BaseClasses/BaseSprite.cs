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
        private Color color;

        protected abstract void OnAnimate(GameTime time);

        protected void ChangeSource(Point newSource)
        {
            source = newSource;
        }

        protected void ChangeColor(Color newColor)
        {
            color = newColor;
        }

        public Point PixelSize
        {
            get
            {
                return size;
            }
        }

        public ZIndex ZIndex
        {
            get
            {
                return activeZIndex;
            }
        }

        public BaseSprite(SpriteBatch spriteBatch, Texture2D texture, Point source, Point size, ZIndex activeZIndex)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.source = source;
            this.size = size;
            this.activeZIndex = activeZIndex;
            color = Color.White;
        }

        public void Draw(GameTime time, Rectangle destination, ZIndex zIndex)
        {
            OnAnimate(time);

            spriteBatch.Draw(texture, destination, new Rectangle(source, size), color);
        }
    }
}

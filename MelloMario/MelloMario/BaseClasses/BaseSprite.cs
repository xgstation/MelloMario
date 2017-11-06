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
        private bool visible;

        protected abstract void OnAnimate(int time);

        protected void ChangeSource(Point newSource)
        {
            source = newSource;
        }

        protected void ChangeColor(Color newColor)
        {
            color = newColor;
        }

        protected void Toggle()
        {
            visible = !visible;
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
            visible = true;
        }

        public void Draw(int time, Rectangle destination, ZIndex zIndex)
        {
            if (visible && zIndex == activeZIndex)
            {
                OnAnimate(time);

                // TODO
                //switch (zIndex)
                //{
                //    case ZIndex.hud:
                //        offset.X = 0;
                //        offset.Y = 0;
                //        break;
                //    case ZIndex.background0:
                //        offset.X = offset.X / 3;
                //        offset.Y = offset.Y * 2 / 3;
                //        break;
                //    case ZIndex.background1:
                //        offset.X = offset.X / 2;
                //        offset.Y = offset.Y * 3 / 4;
                //        break;
                //    case ZIndex.background2:
                //        offset.X = offset.X * 2 / 3;
                //        break;
                //    case ZIndex.background3:
                //        offset.X = offset.X * 3 / 4;
                //        break;
                //}

                spriteBatch.Draw(texture, destination, new Rectangle(source, size), color);
            }
        }
    }
}

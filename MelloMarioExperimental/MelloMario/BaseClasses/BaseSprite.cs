using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MelloMario.Sprites
{
    abstract class BaseSprite : ISprite
    {
        private Point size;
        private ZIndex zIndex;
        private bool visible;

        protected SpriteBatch spriteBatch;

        protected abstract void OnDraw(int time, Rectangle destination);

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

        protected float LayerDepth
        {
            get
            {
                switch (zIndex)
                {
                    case ZIndex.hud:
                        return 0.1f;
                    case ZIndex.splash:
                        return 0.2f;
                    case ZIndex.foreground:
                        return 0.3f;
                    case ZIndex.level:
                        return 0.4f;
                    case ZIndex.item:
                        return 0.5f;
                    case ZIndex.background3:
                        return 0.6f;
                    case ZIndex.background2:
                        return 0.7f;
                    case ZIndex.background1:
                        return 0.8f;
                    case ZIndex.background0:
                        return 0.9f;
                    default:
                        return float.NaN;
                }
            }
        }

        public BaseSprite(SpriteBatch spriteBatch, Point size, ZIndex zIndex)
        {
            this.spriteBatch = spriteBatch;
            this.size = size;
            this.zIndex = zIndex;
            visible = true;
        }

        public void Draw(int time, Rectangle destination)
        {
            if (visible)
            {
                OnDraw(time, destination);
            }
        }
    }
}

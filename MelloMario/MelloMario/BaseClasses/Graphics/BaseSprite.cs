namespace MelloMario.Graphics.Sprites
{
    #region

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal abstract class BaseSprite : ISprite
    {
        private readonly ZIndex zIndex;
        private bool visible;

        protected BaseSprite(Point size, ZIndex zIndex)
        {
            PixelSize = size;
            this.zIndex = zIndex;
            visible = true;
        }

        protected float LayerDepth
        {
            get
            {
                switch (zIndex)
                {
                    case ZIndex.Hud:
                        return 0.1f;
                    case ZIndex.Splash:
                        return 0.2f;
                    case ZIndex.Foreground:
                        return 0.3f;
                    case ZIndex.Level:
                        return 0.4f;
                    case ZIndex.Item:
                        return 0.5f;
                    case ZIndex.Background3:
                        return 0.6f;
                    case ZIndex.Background2:
                        return 0.7f;
                    case ZIndex.Background1:
                        return 0.8f;
                    case ZIndex.Background0:
                        return 0.9f;
                    default:
                        return float.NaN;
                }
            }
        }

        public Point PixelSize { get; }

        public void Draw(int time, SpriteBatch spriteBatch, Rectangle destination)
        {
            if (visible)
            {
                OnDraw(time, spriteBatch, destination);
            }
        }

        protected abstract void OnDraw(int time, SpriteBatch spriteBatch, Rectangle destination);

        protected void Toggle()
        {
            visible = !visible;
        }
    }
}

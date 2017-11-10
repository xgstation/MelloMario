using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MelloMario.Sprites
{
    abstract class BaseSprite : ISprite
    {
        private Point size;
        private ZIndex activeZIndex;
        private bool visible;

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

        public ZIndex ZIndex
        {
            get
            {
                return activeZIndex;
            }
        }

        public BaseSprite(Point size, ZIndex activeZIndex)
        {
            this.size = size;
            this.activeZIndex = activeZIndex;
            visible = true;
        }

        public void Draw(int time, Rectangle destination, ZIndex zIndex)
        {
            if (visible && zIndex == activeZIndex)
            {
                OnDraw(time, destination);
            }
        }
    }
}

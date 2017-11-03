using Microsoft.Xna.Framework;

namespace MelloMario
{
    abstract class BaseGameObject : IGameObject
    {
        private Point location;
        private Point size;
        private ISprite sprite;

        protected enum ResizeModeX
        {
            Left,
            Center,
            Right
        };

        protected enum ResizeModeY
        {
            Top,
            Center,
            Bottom
        };

        protected IGameWorld World;

        protected abstract void OnUpdate(GameTime time);
        protected abstract void OnSimulation(GameTime time);
        protected abstract void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex);

        protected void Relocate(Point delta)
        {
            location += delta;
        }

        protected void Resize(Point newSize, ResizeModeX modeX, ResizeModeY modeY)
        {
            Point delta = new Point();

            switch (modeX)
            {
                case ResizeModeX.Left:
                    delta.X = 0;
                    break;
                case ResizeModeX.Center:
                    delta.X = (size.X - newSize.X) / 2;
                    break;
                case ResizeModeX.Right:
                    delta.X = size.X - newSize.X;
                    break;
            }
            switch (modeY)
            {
                case ResizeModeY.Top:
                    delta.Y = 0;
                    break;
                case ResizeModeY.Center:
                    delta.Y = (size.Y - newSize.Y) / 2;
                    break;
                case ResizeModeY.Bottom:
                    delta.Y = size.Y - newSize.Y;
                    break;
            }

            Relocate(delta);
            size = newSize;

            World.Move(this);
        }

        protected void ShowSprite(ISprite newSprite, ResizeModeX modeX = ResizeModeX.Center, ResizeModeY modeY = ResizeModeY.Bottom)
        {
            sprite = newSprite;
            Resize(sprite.PixelSize, modeX, modeY);
        }

        protected void HideSprite()
        {
            sprite = null;
        }

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle(location.X, location.Y, size.X, size.Y);
            }
        }

        public BaseGameObject(IGameWorld world, Point location, Point size)
        {
            World = world;
            this.location = location;
            this.size = size;
        }

        public void Update(GameTime time)
        {
            // override OnUpdate for states etc.
            OnUpdate(time);
            // override OnSimulation for movement and collision
            OnSimulation(time);
        }

        public void Draw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
            OnDraw(time, viewport, zIndex);

            if (sprite != null)
            {
                Point offset = viewport.Location;

                switch (zIndex)
                {
                    case ZIndex.background:
                        offset.X = offset.X / 2;
                        break;
                    case ZIndex.background1:
                        offset.X = offset.X / 3;
                        break;
                    case ZIndex.background2:
                        offset.X = offset.X * 2 / 3;
                        break;
                    case ZIndex.background3:
                        offset.X = offset.X * 3 / 4;
                        break;
                }

                sprite.Draw(time, new Rectangle(Boundary.Location - offset, Boundary.Size), zIndex);
            }
        }
    }
}

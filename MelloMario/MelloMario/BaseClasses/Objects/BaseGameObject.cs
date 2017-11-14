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

        // TODO: World should only be used in base classes and PlayerMario
        protected IGameWorld world;

        protected abstract void OnUpdate(int time);
        protected abstract void OnSimulation(int time);
        protected abstract void OnDraw(int time, Rectangle viewport);

        protected virtual void RemoveSelf()
        {
            world.Remove(this);
        }
        protected void Relocate(Point newLocation)
        {
            location = newLocation;
            world.Move(this);
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

            Relocate(location + delta);
            size = newSize;

            world.Move(this);
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
            this.location = location;
            this.size = size;
            this.world = world;
            this.world.Add(this);

        }

        public void Update(int time)
        {
            // override OnUpdate for states etc.
            OnUpdate(time);
            // override OnSimulation for movement and collision
            OnSimulation(time);
        }

        public void Draw(int time, Rectangle viewport)
        {
            if (sprite != null)
            {
                OnDraw(time, viewport);

                sprite.Draw(time, new Rectangle(Boundary.Location - viewport.Location, Boundary.Size));
            }
        }
    }
}

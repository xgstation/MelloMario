namespace MelloMario.Objects
{
    #region

    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal abstract class BaseGameObject : IGameObject
    {
        protected enum ResizeModeX
        {
            Left,
            Center,
            Right
        }

        protected enum ResizeModeY
        {
            Top,
            Center,
            Bottom
        }

        // note: World should only be used in base classes and MarioCharacter
        protected IWorld World;

        public Point Location { get; protected set; }
        private Point size;
        private ISprite sprite;

        protected BaseGameObject(IWorld world, Point location, Point size)
        {
            this.Location = location;
            this.size = size;
            World = world;
        }

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle(Location.X, Location.Y, size.X, size.Y);
            }
        }

        public void Update(int time)
        {
            // override OnUpdate for states etc.
            OnUpdate(time);
            // override OnSimulation for movement and collision
            OnSimulation(time);
        }

        public void Draw(int time, SpriteBatch spriteBatch)
        {
            sprite?.Draw(time, spriteBatch, new Rectangle(Boundary.Location, Boundary.Size));
        }

        protected abstract void OnUpdate(int time);
        protected abstract void OnSimulation(int time);

        protected void Relocate(Point newLocation)
        {
            Location = newLocation;
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

            Relocate(Location + delta);
            size = newSize;

            World.Move(this);
        }

        protected void ShowSprite(
            ISprite newSprite,
            ResizeModeX modeX = ResizeModeX.Center,
            ResizeModeY modeY = ResizeModeY.Bottom)
        {
            sprite = newSprite;
            Resize(sprite.PixelSize, modeX, modeY);
        }

        protected void HideSprite()
        {
            sprite = null;
        }
    }
}

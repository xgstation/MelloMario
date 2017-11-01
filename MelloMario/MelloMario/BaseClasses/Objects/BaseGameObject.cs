using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MelloMario
{
    abstract class BaseGameObject : IGameObject
    {
        private Point location;
        private Point size;
        private ISprite sprite;

        protected enum ResizeModeX { Left, Center, Right };
        protected enum ResizeModeY { Top, Center, Bottom };

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

            World.MoveObject(this);
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
            this.World = world;
            this.location = location;
            this.size = size;
        }

        public void Update(GameTime time)
        {
            // TODO: override OnUpdate for states etc.
            //       override OnSimulation for movement and collision
            OnUpdate(time);
            OnSimulation(time);
        }

        public void Draw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
            OnDraw(time, viewport, zIndex);

            if (sprite != null)
            {
                if (zIndex == ZIndex.background)
                {
                    Point location = viewport.Location;
                    location.X = (int)(location.X * 0.5);
                    viewport.Location = location;
                }
                else if (zIndex == ZIndex.background2)
                {
                    Point location = viewport.Location;
                    location.X = (int)(location.X * 0.65);
                    viewport.Location = location;
                }
                else if (zIndex == ZIndex.background3)
                {
                    Point location = viewport.Location;
                    location.X = (int)(location.X * 0.82);
                    viewport.Location = location;
                }

                sprite.Draw(time, new Rectangle(Boundary.Location - viewport.Location, Boundary.Size), zIndex);
            }
        }
    }
}

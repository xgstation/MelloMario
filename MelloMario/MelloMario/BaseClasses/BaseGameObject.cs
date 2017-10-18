using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MelloMario
{
    abstract class BaseGameObject : IGameObject
    {
        private IGameWorld world;
        private Point location;
        private Point size;
        private Point movement;
        private ISprite sprite;

        private IEnumerable<Tuple<CollisionMode, CollisionMode>> ScanCollideModes(Rectangle targetBoundary)
        {
            Rectangle rectA = Boundary;
            Rectangle rectB = targetBoundary;

            bool intersectX = rectA.Left < rectB.Right && rectB.Left < rectA.Right;
            bool intersectY = rectA.Top < rectB.Bottom && rectB.Top < rectA.Bottom;

            if (intersectY && rectA.Left == rectB.Right)
            {
                yield return new Tuple<CollisionMode, CollisionMode>(CollisionMode.Left, CollisionMode.Right);
            }
            if (intersectY && rectA.Right == rectB.Left)
            {
                yield return new Tuple<CollisionMode, CollisionMode>(CollisionMode.Right, CollisionMode.Left);
            }
            if (intersectX && rectA.Top == rectB.Bottom)
            {
                yield return new Tuple<CollisionMode, CollisionMode>(CollisionMode.Top, CollisionMode.Bottom);
            }
            if (intersectX && rectA.Bottom == rectB.Top)
            {
                yield return new Tuple<CollisionMode, CollisionMode>(CollisionMode.Bottom, CollisionMode.Top);
            }
            if (intersectY && rectA.Left == rectB.Left)
            {
                yield return new Tuple<CollisionMode, CollisionMode>(CollisionMode.InnerLeft, CollisionMode.InnerLeft);
            }
            if (intersectY && rectA.Right == rectB.Right)
            {
                yield return new Tuple<CollisionMode, CollisionMode>(CollisionMode.InnerRight, CollisionMode.InnerRight);
            }
            if (intersectX && rectA.Top == rectB.Top)
            {
                yield return new Tuple<CollisionMode, CollisionMode>(CollisionMode.InnerTop, CollisionMode.InnerTop);
            }
            if (intersectX && rectA.Bottom == rectB.Bottom)
            {
                yield return new Tuple<CollisionMode, CollisionMode>(CollisionMode.InnerBottom, CollisionMode.InnerBottom);
            }
        }

        private void CollideAll()
        {
            foreach (IGameObject target in world.ScanNearbyObjects(this))
            {
                foreach (Tuple<CollisionMode, CollisionMode> pair in ScanCollideModes(target.Boundary))
                {
                    OnCollision(target, pair.Item1);

                    if (target is BaseGameObject)
                    {
                        ((BaseGameObject)target).OnCollision(this, pair.Item2);
                    }
                }
            }

            foreach (Tuple<CollisionMode, CollisionMode> pair in ScanCollideModes(world.Boundary))
            {
                OnOut(pair.Item1);
            }
        }

        protected enum CollisionMode { Left, Right, Top, Bottom, InnerLeft, InnerRight, InnerTop, InnerBottom };
        protected enum ResizeModeX { Left, Center, Right };
        protected enum ResizeModeY { Top, Center, Bottom };

        protected abstract void OnSimulation(GameTime time);
        protected abstract void OnCollision(IGameObject target, CollisionMode mode);
        protected abstract void OnOut(CollisionMode mode);

        protected void Resize(Point newSize, ResizeModeX modeX, ResizeModeY modeY)
        {
            int moveX = 0;
            int moveY = 0;

            switch (modeX)
            {
                case ResizeModeX.Left:
                    moveX = 0;
                    break;
                case ResizeModeX.Center:
                    moveX = (size.X - newSize.X) / 2;
                    break;
                case ResizeModeX.Right:
                    moveX = size.X - newSize.X;
                    break;
            }
            switch (modeY)
            {
                case ResizeModeY.Top:
                    moveY = 0;
                    break;
                case ResizeModeY.Center:
                    moveY = (size.Y - newSize.Y) / 2;
                    break;
                case ResizeModeY.Bottom:
                    moveY = size.Y - newSize.Y;
                    break;
            }

            size = newSize;
            location.X += moveX;
            location.Y += moveY;
        }

        // TODO: remove them later
        protected IGameWorld World()
        {
            return world;
        }

        protected void Move(Point delta)
        {
            movement += delta;
        }

        protected void StopHorizontalMovement()
        {
            movement.X = 0;
        }

        protected void StopVerticalMovement()
        {
            movement.Y = 0;
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

        protected void RemoveSelf()
        {
            StopHorizontalMovement();
            StopVerticalMovement();
            world.RemoveObject(this);
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
            this.world = world;
            this.location = location;
            this.size = size;

            movement = new Point();
        }

        public void Update(GameTime time)
        {
            OnSimulation(time);

            CollideAll();

            // Since each update is a very small iteration, the order does not matter.
            while (movement.X > 0)
            {
                location.X += 1;
                movement.X -= 1;
                CollideAll();
            }
            while (movement.X < 0)
            {
                location.X -= 1;
                movement.X += 1;
                CollideAll();
            }
            while (movement.Y > 0)
            {
                location.Y += 1;
                movement.Y -= 1;
                CollideAll();
            }
            while (movement.Y < 0)
            {
                location.Y -= 1;
                movement.Y += 1;
                CollideAll();
            }
        }

        public void Draw(GameTime time)
        {
            if (sprite != null)
            {
                sprite.Draw(time, Boundary);
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MelloMario
{
    abstract class BaseGameObject : IGameObject
    {
        private Point location;
        private Point size;
        private Point movement;
        private ISprite sprite;

        private void Collide(IGameObject target)
        {
            Rectangle rectA = Boundary;
            Rectangle rectB = target.Boundary;

            bool intersectX = rectA.Left < rectB.Right && rectB.Left < rectA.Right;
            bool intersectY = rectA.Top < rectB.Bottom && rectB.Top < rectA.Bottom;

            if (intersectY && rectA.Left == rectB.Right)
            {
                OnCollision(target, CollisionMode.Left);

                if (target is BaseGameObject)
                {
                    ((BaseGameObject)target).OnCollision(this, CollisionMode.Right);
                }
            }
            if (intersectY && rectA.Right == rectB.Left)
            {
                OnCollision(target, CollisionMode.Right);

                if (target is BaseGameObject)
                {
                    ((BaseGameObject)target).OnCollision(this, CollisionMode.Left);
                }
            }
            if (intersectX && rectA.Top == rectB.Bottom)
            {
                OnCollision(target, CollisionMode.Top);

                if (target is BaseGameObject)
                {
                    ((BaseGameObject)target).OnCollision(this, CollisionMode.Bottom);
                }
            }
            if (intersectY && rectA.Bottom == rectB.Top)
            {
                OnCollision(target, CollisionMode.Bottom);

                if (target is BaseGameObject)
                {
                    ((BaseGameObject)target).OnCollision(this, CollisionMode.Top);
                }
            }
            if (intersectX && intersectY)
            {
                OnCollision(target, CollisionMode.Cross);

                if (target is BaseGameObject)
                {
                    ((BaseGameObject)target).OnCollision(this, CollisionMode.Cross);
                }
            }
        }

        private void CollideAll(IEnumerable<IGameObject> collidable)
        {
            foreach (IGameObject target in collidable)
            {
                Collide(target);
            }
        }

        protected enum CollisionMode { Left, Right, Top, Bottom, Cross };
        protected enum ResizeModeX { Left, Center, Right };
        protected enum ResizeModeY { Top, Center, Bottom };

        protected abstract void OnSimulation(GameTime time);
        protected abstract void OnCollision(IGameObject target, CollisionMode mode);

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

        protected void Move(Point delta)
        {
            movement += delta;
        }

        protected void StopMove()
        {
            movement.X = 0;
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

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle(location.X, location.Y, size.X, size.Y);
            }
        }

        public BaseGameObject(Point location, Point size)
        {
            this.location = location;
            this.size = size;

            movement = new Point();
        }

        public void Update(GameTime time, IEnumerable<IGameObject> collidable)
        {
            OnSimulation(time);

            CollideAll(collidable);

            // Since each update is a very small iteration, the order does not matter.
            while (movement.X > 0)
            {
                location.X += 1;
                movement.X -= 1;
                CollideAll(collidable);
            }
            while (movement.X < 0)
            {
                location.X -= 1;
                movement.X += 1;
                CollideAll(collidable);
            }
            while (movement.Y > 0)
            {
                location.Y += 1;
                movement.Y -= 1;
                CollideAll(collidable);
            }
            while (movement.Y < 0)
            {
                location.Y -= 1;
                movement.Y += 1;
                CollideAll(collidable);
            }
        }

        public void Draw(GameTime time, SpriteBatch spriteBatch)
        {
            if (sprite != null)
            {
                sprite.Draw(time, spriteBatch, Boundary);
            }
        }
    }
}

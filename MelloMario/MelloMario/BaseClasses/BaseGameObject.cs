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
            if (rectA.Left == rectB.Right || rectA.Right == rectB.Left || rectA.Top == rectB.Bottom || rectA.Bottom == rectB.Top)
            {
                OnCollision(target);

                if (target is BaseGameObject)
                {
                    ((BaseGameObject)target).OnCollision(this);
                }
            }
        }

        private void CollideAll(IList<IGameObject> collidable)
        {
            foreach (IGameObject target in collidable)
            {
                Collide(target);
            }
        }

        protected enum ResizeModeX { Left, Center, Right };
        protected enum ResizeModeY { Top, Center, Bottom };

        protected abstract void OnSimulation(GameTime time);
        protected abstract void OnCollision(IGameObject target);

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

        protected void ShowSprite(ISprite sprite)
        {
            this.sprite = sprite;
        }

        protected void ShowSprite(ISprite sprite, ResizeModeX modeX, ResizeModeY modeY)
        {
            ShowSprite(sprite);
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

        public void Update(GameTime time, IList<IGameObject> collidable)
        {
            OnSimulation(time);

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

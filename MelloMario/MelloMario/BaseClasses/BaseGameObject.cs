using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MelloMario
{
    abstract class BaseGameObject : IGameObject
    {
        private Point Location;
        private Point Size;
        private Point Movement;
        private ISprite Sprite;

        private void Collide(IGameObject target)
        {
            Rectangle rectA = Boundary;
            Rectangle rectB = target.Boundary;
            if (rectA.Left == rectB.Right || rectA.Right == rectB.Left || rectA.Top == rectB.Bottom || rectA.Bottom == rectB.Top)
            {
                OnCollision(target);
                target.OnCollision(this);
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
                    moveX = (Size.X - newSize.X) / 2;
                    break;
                case ResizeModeX.Right:
                    moveX = Size.X - newSize.X;
                    break;
            }
            switch (modeY)
            {
                case ResizeModeY.Top:
                    moveY = 0;
                    break;
                case ResizeModeY.Center:
                    moveY = (Size.Y - newSize.Y) / 2;
                    break;
                case ResizeModeY.Bottom:
                    moveY = Size.Y - newSize.Y;
                    break;
            }

            Size = newSize;
            Location.X += moveX;
            Location.Y += moveY;
        }

        protected void Move(Point newMovement)
        {
            Movement += newMovement;
        }

        protected void StopMove()
        {
            Movement.X = 0;
            Movement.Y = 0;
        }

        protected void ChangeSprite(ISprite sprite)
        {
            Sprite = sprite;
        }

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle(Location.X, Location.Y, Size.X, Size.Y);
            }
        }

        public BaseGameObject(Point location, Point size)
        {
            this.Location = location;
            this.Size = size;
            this.Movement = new Point();
        }

        public void Update(GameTime time, IList<IGameObject> collidable)
        {
            OnSimulation(time);

            // Since each update is a very small iteration, the order does not matter.
            while (Movement.X > 0)
            {
                Location.X += 1;
                Movement.X -= 1;
                CollideAll(collidable);
            }
            while (Movement.X < 0)
            {
                Location.X -= 1;
                Movement.X += 1;
                CollideAll(collidable);
            }
            while (Movement.Y > 0)
            {
                Location.Y += 1;
                Movement.Y -= 1;
                CollideAll(collidable);
            }
            while (Movement.Y < 0)
            {
                Location.Y -= 1;
                Movement.Y += 1;
                CollideAll(collidable);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Boundary);
        }

        public abstract void OnSimulation(GameTime time);
        public abstract void OnCollision(IGameObject target);
    }
}

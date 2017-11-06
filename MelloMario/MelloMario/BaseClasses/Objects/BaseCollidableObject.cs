using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MelloMario
{
    abstract class BaseCollidableObject : BaseGameObject
    {
        private Point movement;

        private IEnumerable<Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>> ScanCollideModes(Rectangle targetBoundary)
        {
            Rectangle rectA = Boundary;
            Rectangle rectB = targetBoundary;

            if (rectA.Top < rectB.Bottom && rectB.Top < rectA.Bottom)
            {
                CornerMode cornerY =
                    rectA.Center.Y <= rectB.Top
                        ? CornerMode.Top
                        : rectA.Center.Y >= rectB.Bottom
                            ? CornerMode.Bottom
                            : CornerMode.Center;
                CornerMode cornerYPassive =
                    rectB.Center.Y <= rectA.Top
                        ? CornerMode.Top
                        : rectB.Center.Y >= rectA.Bottom
                            ? CornerMode.Bottom
                            : CornerMode.Center;

                if (rectA.Left == rectB.Right)
                {
                    yield return new Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>(
                        CollisionMode.Left, CollisionMode.Right, cornerY, cornerYPassive
                    );
                }
                if (rectA.Right == rectB.Left)
                {
                    yield return new Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>(
                        CollisionMode.Right, CollisionMode.Left, cornerY, cornerYPassive
                    );
                }
                if (rectA.Left == rectB.Left)
                {
                    yield return new Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>(
                        CollisionMode.InnerLeft, CollisionMode.InnerLeft, cornerY, cornerYPassive
                    );
                }
                if (rectA.Right == rectB.Right)
                {
                    yield return new Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>(
                        CollisionMode.InnerRight, CollisionMode.InnerRight, cornerY, cornerYPassive
                    );
                }
            }

            if (rectA.Left < rectB.Right && rectB.Left < rectA.Right)
            {
                CornerMode cornerX =
                    rectA.Center.X <= rectB.Left
                        ? CornerMode.Left
                        : rectA.Center.X >= rectB.Right
                            ? CornerMode.Right
                            : CornerMode.Center;
                CornerMode cornerXPassive =
                    rectB.Center.X <= rectA.Left
                        ? CornerMode.Left
                        : rectB.Center.X >= rectA.Right
                            ? CornerMode.Right
                            : CornerMode.Center;

                if (rectA.Top == rectB.Bottom)
                {
                    yield return new Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>(
                        CollisionMode.Top, CollisionMode.Bottom, cornerX, cornerXPassive
                    );
                }
                if (rectA.Bottom == rectB.Top)
                {
                    yield return new Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>(
                        CollisionMode.Bottom, CollisionMode.Top, cornerX, cornerXPassive
                    );
                }
                if (rectA.Top == rectB.Top)
                {
                    yield return new Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>(
                        CollisionMode.InnerTop, CollisionMode.InnerTop, cornerX, cornerXPassive
                    );
                }
                if (rectA.Bottom == rectB.Bottom)
                {
                    yield return new Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>(
                        CollisionMode.InnerBottom, CollisionMode.InnerBottom, cornerX, cornerXPassive
                    );
                }
            }
        }

        private void CollideAll()
        {
            foreach (IGameObject target in World.ScanNearby(Boundary))
            {
                if (target != this && target is BaseCollidableObject obj)
                {
                    foreach (Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode> pair in ScanCollideModes(target.Boundary))
                    {
                        OnCollision(target, pair.Item1, pair.Item3, pair.Item4);
                        obj.OnCollision(this, pair.Item2, pair.Item4, pair.Item3);
                    }
                }
            }

            foreach (Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode> pair in ScanCollideModes(World.Boundary))
            {
                OnCollideCamera(pair.Item1);
            }
        }

        protected enum CollisionMode
        {
            Left,
            Right,
            Top,
            Bottom,
            InnerLeft,
            InnerRight,
            InnerTop,
            InnerBottom
        };

        protected enum CornerMode
        {
            Left,
            Right,
            Top,
            Bottom,
            Center
        };

        protected abstract void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive);
        protected abstract void OnCollideViewport(IPlayer player, CollisionMode mode);
        protected abstract void OnCollideCamera(CollisionMode mode);

        protected void Move(Point delta)
        {
            movement += delta;

            World.Move(this);
        }

        protected void StopHorizontalMovement()
        {
            movement.X = 0;
        }

        protected void StopVerticalMovement()
        {
            movement.Y = 0;
        }

        protected void RemoveSelf()
        {
            StopHorizontalMovement();
            StopVerticalMovement();

            World.Remove(this);
        }

        protected override void OnSimulation(GameTime time)
        {
            if (movement.X != 0 || movement.Y != 0)
            {
                CollideAll();

                // Since each update is a very small iteration, the order does not matter.

                while (movement.X < 0)
                {
                    Relocate(new Point(-1, 0));
                    movement.X += 1;
                    CollideAll();
                }

                while (movement.X > 0)
                {
                    Relocate(new Point(1, 0));
                    movement.X -= 1;
                    CollideAll();
                }

                while (movement.Y < 0)
                {
                    Relocate(new Point(0, -1));
                    movement.Y += 1;
                    CollideAll();
                }

                while (movement.Y > 0)
                {
                    Relocate(new Point(0, 1));
                    movement.Y -= 1;
                    CollideAll();
                }
            }
        }

        public BaseCollidableObject(IGameWorld world, Point location, Point size) : base(world, location, size)
        {
            movement = new Point();
        }
    }
}

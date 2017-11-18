using System;
using System.Collections.Generic;
using MelloMario.Theming;
using Microsoft.Xna.Framework;

namespace MelloMario
{
    internal abstract class BaseCollidableObject : BaseGameObject
    {
        public delegate void LivesHandler(BaseCollidableObject m, GameEventArgs e);

        public delegate void PointHandler(BaseCollidableObject m, GameEventArgs e);

        private GameEventArgs gameEventInfo;
        private Point movement;

        public BaseCollidableObject(IGameWorld world, Point location, IListener listener, Point size) : base(world,
            location, size)
        {
            listener?.Subscribe(this);
            movement = new Point();
        }

        public event PointHandler HandlerPoints;
        public event LivesHandler HandlerLives;

        private IEnumerable<Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>> ScanCollideModes(
            Rectangle targetBoundary)
        {
            var rectA = Boundary;
            var rectB = targetBoundary;

            if (rectA.Top < rectB.Bottom && rectB.Top < rectA.Bottom)
            {
                var cornerY = rectA.Center.Y <= rectB.Top
                    ? CornerMode.Top
                    : rectA.Center.Y >= rectB.Bottom
                        ? CornerMode.Bottom
                        : CornerMode.Center;
                var cornerYPassive = rectB.Center.Y <= rectA.Top
                    ? CornerMode.Top
                    : rectB.Center.Y >= rectA.Bottom
                        ? CornerMode.Bottom
                        : CornerMode.Center;

                if (rectA.Left == rectB.Right)
                    yield return new Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>(CollisionMode.Left,
                        CollisionMode.Right, cornerY, cornerYPassive);
                if (rectA.Right == rectB.Left)
                    yield return new Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>(CollisionMode.Right,
                        CollisionMode.Left, cornerY, cornerYPassive);
                if (rectA.Left == rectB.Left)
                    yield return new Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>(
                        CollisionMode.InnerLeft, CollisionMode.InnerLeft, cornerY, cornerYPassive);
                if (rectA.Right == rectB.Right)
                    yield return new Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>(
                        CollisionMode.InnerRight, CollisionMode.InnerRight, cornerY, cornerYPassive);
            }

            if (rectA.Left < rectB.Right && rectB.Left < rectA.Right)
            {
                var cornerX = rectA.Center.X <= rectB.Left
                    ? CornerMode.Left
                    : rectA.Center.X >= rectB.Right
                        ? CornerMode.Right
                        : CornerMode.Center;
                var cornerXPassive = rectB.Center.X <= rectA.Left
                    ? CornerMode.Left
                    : rectB.Center.X >= rectA.Right
                        ? CornerMode.Right
                        : CornerMode.Center;

                if (rectA.Top == rectB.Bottom)
                    yield return new Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>(CollisionMode.Top,
                        CollisionMode.Bottom, cornerX, cornerXPassive);
                if (rectA.Bottom == rectB.Top)
                    yield return new Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>(CollisionMode.Bottom,
                        CollisionMode.Top, cornerX, cornerXPassive);
                if (rectA.Top == rectB.Top)
                    yield return new Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>(CollisionMode.InnerTop,
                        CollisionMode.InnerTop, cornerX, cornerXPassive);
                if (rectA.Bottom == rectB.Bottom)
                    yield return new Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode>(
                        CollisionMode.InnerBottom, CollisionMode.InnerBottom, cornerX, cornerXPassive);
            }
        }

        private void CollideAll()
        {
            foreach (var target in World.ScanNearby(Boundary))
                if (target != this && target is BaseCollidableObject obj)
                    foreach (var pair in ScanCollideModes(target.Boundary))
                    {
                        OnCollision(target, pair.Item1, pair.Item2, pair.Item3, pair.Item4);
                        obj.OnCollision(this, pair.Item2, pair.Item1, pair.Item4, pair.Item3);
                    }

            // TODO: build a list of players that observed this objects
            //foreach (IPlayer player in session.???)
            //{
            //    foreach (Tuple<CollisionMode, CollisionMode, CornerMode, CornerMode> pair in ScanCollideModes(World.Boundary))
            //    {
            //        OnCollideViewport(player, pair.Item1, pair.Item2);
            //    }
            //}

            foreach (var pair in ScanCollideModes(World.Boundary))
                OnCollideWorld(pair.Item1, pair.Item2);
        }

        protected abstract void OnCollision(IGameObject target, CollisionMode mode, CollisionMode modePassive,
            CornerMode corner, CornerMode cornerPassive);

        protected abstract void OnCollideViewport(IPlayer player, CollisionMode mode, CollisionMode modePassive);
        protected abstract void OnCollideWorld(CollisionMode mode, CollisionMode modePassive);

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

        protected override void OnSimulation(int time)
        {
            float offset = 0;

            while (true)
            {
                if (movement.X == 0 && movement.Y == 0)
                    break;

                CollideAll();

                if (movement.X == 0 && movement.Y == 0)
                    break;

                float sqrX = movement.X * movement.X;
                float sqrY = movement.Y * movement.Y;
                float offsetX = (float) Math.Sqrt(sqrY / (sqrX + sqrY));
                float offsetY = (float) Math.Sqrt(sqrX / (sqrX + sqrY));

                var location = Boundary.Location;
                if (Math.Abs(offset + offsetX) < Math.Abs(offset - offsetY))
                    if (movement.X < 0)
                    {
                        location.X -= 1;
                        movement.X += 1;
                    }
                    else
                    {
                        location.X += 1;
                        movement.X -= 1;
                    }
                else
                {
                    if (movement.Y < 0)
                    {
                        location.Y -= 1;
                        movement.Y += 1;
                    }
                    else
                    {
                        location.Y += 1;
                        movement.Y -= 1;
                    }
                }

                Relocate(location);
            }
        }

        protected void ScorePoints(int points)
        {
            gameEventInfo = new GameEventArgs {Points = points};

            HandlerPoints?.Invoke(this, gameEventInfo);
        }

        protected void ChangeLives()
        {
            gameEventInfo = new GameEventArgs {Points = 1};

            HandlerLives?.Invoke(this, gameEventInfo);
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
        }

        protected enum CornerMode
        {
            Left,
            Right,
            Top,
            Bottom,
            Center
        }
    }
}
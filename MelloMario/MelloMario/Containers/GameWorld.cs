using System.Collections.Generic;
using MelloMario.Theming;
using Microsoft.Xna.Framework;

namespace MelloMario.Containers
{
    internal class GameWorld : BaseContainer<Point, IGameObject>, IGameWorld
    {
        private readonly Point initialPoint;
        private readonly ISet<Point> respawnPoints;
        private Point size;

        public GameWorld(string id, Point size, Point initial, IEnumerable<Point> respawn)
        {
            Id = id;
            this.size = size;

            initialPoint = new Point(initial.X * GameConst.GRID, initial.Y * GameConst.GRID);
            respawnPoints = new HashSet<Point>();
            foreach (Point p in respawn)
            {
                respawnPoints.Add(new Point(p.X * GameConst.GRID, p.Y * GameConst.GRID));
            }
        }

        public string Id { get; }

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle(0, 0, size.X * GameConst.GRID, size.Y * GameConst.GRID);
            }
        }

        public void Extend(int x, int y)
        {
            size.X += x;
            size.Y += y;
        }

        public IEnumerable<IGameObject> ScanNearby(Rectangle range)
        {
            int left = (range.Left - GameConst.SCANRANGE) / GameConst.GRID;
            int right = (range.Right + GameConst.SCANRANGE) / GameConst.GRID;
            int top = (range.Top - GameConst.SCANRANGE) / GameConst.GRID;
            int bottom = (range.Bottom + GameConst.SCANRANGE) / GameConst.GRID;

            for (int i = left; i <= right; ++i)
                for (int j = top; j <= bottom; ++j)
                {
                    foreach (IGameObject obj in Scan(new Point(i, j)))
                    {
                        yield return obj;
                    }
                }
        }

        public Point GetInitialPoint()
        {
            return initialPoint;
        }

        public Point GetRespawnPoint(Point location)
        {
            Point target = location;

            foreach (Point p in respawnPoints)
            {
                if (p.X <= location.X && p.X > target.X)
                {
                    target = p;
                }
            }

            return target;
        }

        protected override Point GetKey(IGameObject value)
        {
            Point center = value.Boundary.Center;
            return new Point(center.X / GameConst.GRID, center.Y / GameConst.GRID);
        }
    }
}

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MelloMario.Containers
{
    class GameWorld : BaseContainer<Point, IGameObject>, IGameWorld
    {
        private const int GRID = 32;
        private const int SCANRANGE = 24;

        private Point size;
        private Point initialPoint;
        private ISet<Point> respawnPoints;
        

        protected override Point GetKey(IGameObject value)
        {
            Point center = value.Boundary.Center;
            return new Point(center.X / GRID, center.Y / GRID);
        }

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle(0, 0, size.X * GRID, size.Y * GRID);
            }
        }

        public GameWorld(Point size, Point initial, IEnumerable<Point> respawn)
        {
            this.size = size;

            initialPoint = new Point(initial.X * GRID, initial.Y * GRID);
            respawnPoints = new HashSet<Point>();
            foreach (Point p in respawn)
            {
                respawnPoints.Add(new Point(p.X * GRID, p.Y * GRID));
            }

        }

        public IEnumerable<IGameObject> ScanNearby(Rectangle range)
        {
            int left = (range.Left - SCANRANGE) / GRID;
            int right = (range.Right + SCANRANGE) / GRID;
            int top = (range.Top - SCANRANGE) / GRID;
            int bottom = (range.Bottom + SCANRANGE) / GRID;

            for (int i = left; i <= right; ++i)
            {
                for (int j = top; j <= bottom; ++j)
                {
                    foreach (IGameObject obj in Scan(new Point(i, j)))
                    {
                        yield return obj;
                    }
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
    }
}

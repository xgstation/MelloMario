using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MelloMario.Containers
{
    class GameWorld : BaseContainer<Point, IGameObject>, IGameWorld
    {
        private int grid;
        private Point size;

        protected override Point getKey(IGameObject value)
        {
            Point center = value.Boundary.Center;
            return new Point(center.X / grid, center.Y / grid);
        }

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle(0, 0, size.X * grid, size.Y * grid);
            }
        }

        public GameWorld(int grid, Point size)
        {
            this.grid = grid;
            this.size = size;
        }

        public IEnumerable<IGameObject> ScanNearby(Rectangle range)
        {
            int left = range.Left / grid - 2;
            int right = range.Right / grid + 2;
            int top = range.Top / grid - 2;
            int bottom = range.Bottom / grid + 2;

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

        private ISet<Point> respawnPoints; // TODO: temporary code
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

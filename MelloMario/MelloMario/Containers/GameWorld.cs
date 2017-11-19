using System.Collections.Generic;
using MelloMario.Theming;
using Microsoft.Xna.Framework;

namespace MelloMario.Containers
{
    internal class GameWorld : BaseContainer<Point, IGameObject>, IGameWorld
    {
        private readonly Point initialPoint;
        private readonly ISet<Point> respawnPoints;
        private Point extended;
        private Point originalSize;

        public GameWorld(string id, Point originalSize, Point initial, IEnumerable<Point> respawn)
        {
            Id = id;
            this.originalSize = originalSize;
            extended = Point.Zero;

            initialPoint = new Point(initial.X * GameConst.GRID, initial.Y * GameConst.GRID);
            respawnPoints = new HashSet<Point>();
            foreach (var p in respawn)
            {
                respawnPoints.Add(new Point(p.X * GameConst.GRID, p.Y * GameConst.GRID));
            }
        }

        public string Id { get; }

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle(0, 0, (originalSize.X + extended.X) * GameConst.GRID,
                    (originalSize.Y + extended.Y) * GameConst.GRID);
            }
        }

        public void Extend(int x, int y)
        {
            extended.X += x;
            extended.Y += y;
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
                foreach (var obj in Scan(new Point(i, j)))
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
            var target = location;

            foreach (var p in respawnPoints)
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
            var center = value.Boundary.Center;
            return new Point(center.X / GameConst.GRID, center.Y / GameConst.GRID);
        }
    }
}
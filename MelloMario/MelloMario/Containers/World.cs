namespace MelloMario.Containers
{
    #region

    using System;
    using System.Collections.Generic;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class World : BaseContainer<Point, IGameObject>, IWorld
    {
        private readonly IGenerator generator;
        private readonly ISet<Point> respawnPoints;

        public World(string id, WorldType type, IGenerator generator, IEnumerable<Point> respawn)
        {
            ID = id;
            Type = type;

            this.generator = generator;

            respawnPoints = new HashSet<Point>();
            foreach (Point p in respawn)
            {
                respawnPoints.Add(new Point(p.X, p.Y));
            }
        }

        public string ID { get; }
        public WorldType Type { get; }
        public Rectangle Boundary { get; private set; }

        public void Extend(int left, int right, int top, int bottom)
        {
            Boundary = new Rectangle(Boundary.Left - left, Boundary.Top - top, Boundary.Width + left + right, Boundary.Height + top + bottom);
        }

        public IEnumerable<IGameObject> ScanNearby(Rectangle range, bool allowExtension = false)
        {
            if (allowExtension)
            {
                generator.Request(this, range);
            }

            int left = (range.Left - Const.SCAN_RANGE) / Const.GRID;
            int right = (range.Right + Const.SCAN_RANGE) / Const.GRID;
            int top = (range.Top - Const.SCAN_RANGE) / Const.GRID;
            int bottom = (range.Bottom + Const.SCAN_RANGE) / Const.GRID;

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

        public Point GetRespawnPoint(Point location)
        {
            Point target = location;

            foreach (Point p in respawnPoints)
            {
                if (p.X <= location.X && p.X >= target.X)
                {
                    target = p;
                }
            }

            return target;
        }

        protected override Point GetKey(IGameObject value)
        {
            Point center = value.Boundary.Center;

            return new Point(center.X / Const.GRID, center.Y / Const.GRID);
        }
    }
}

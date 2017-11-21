namespace MelloMario.Containers
{
    #region

    using System;
    using System.Collections.Generic;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    [Serializable]
    internal class World : BaseContainer<Point, IGameObject>, IWorld
    {
        private readonly Point initialPoint;
        private readonly ISet<Point> respawnPoints;

        public World(string id, WorldType type, Point size, Point initial, IEnumerable<Point> respawn)
        {
            ID = id;
            Type = type;
            Boundary = new Rectangle(0, 0, size.X * Const.GRID, size.Y * Const.GRID);

            initialPoint = new Point(initial.X * Const.GRID, initial.Y * Const.GRID);
            respawnPoints = new HashSet<Point>();
            foreach (Point p in respawn)
            {
                respawnPoints.Add(new Point(p.X * Const.GRID, p.Y * Const.GRID));
            }
        }

        public string ID { get; }
        public WorldType Type { get; }
        public Rectangle Boundary { get; private set; }

        public void Extend(int left, int right, int top, int bottom)
        {
            Boundary = new Rectangle(Boundary.Left - left, Boundary.Top - top, Boundary.Width + left + right, Boundary.Height + top + bottom);
        }

        public IEnumerable<IGameObject> ScanNearby(Rectangle range)
        {
            int left = (range.Left - Const.SCANRANGE) / Const.GRID;
            int right = (range.Right + Const.SCANRANGE) / Const.GRID;
            int top = (range.Top - Const.SCANRANGE) / Const.GRID;
            int bottom = (range.Bottom + Const.SCANRANGE) / Const.GRID;

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

        protected override Point GetKey(IGameObject value)
        {
            Point center = value.Boundary.Center;

            return new Point(center.X / Const.GRID, center.Y / Const.GRID);
        }
    }
}

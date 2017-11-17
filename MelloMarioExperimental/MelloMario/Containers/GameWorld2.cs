using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using MelloMario.Collision;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Containers
{
    class GameWorld2 : IGameWorld
    {
        private readonly QuadTree<IGameObject> objContainer;
        private string id;
        private readonly Point worldSize;
        private readonly Point initialPoint;
        private ISet<Point> respawnPoints;


        public GameWorld2(string id, Point worldSize, Point initialPoint, IEnumerable<Point> respawnPoints)
        {
            FlagIsTouched = false;
            Id = id;
            this.worldSize = worldSize;
            this.initialPoint = new Point(initialPoint.X * GameConst.GRID, initialPoint.Y * GameConst.GRID);
            respawnPoints = new List<Point>();

            foreach (Point respawnPoint in respawnPoints)
            {
                this.respawnPoints.Add(respawnPoint);
            }
            objContainer = new QuadTree<IGameObject>(new Rectangle(0, 0, worldSize.X * GameConst.GRID, worldSize.Y * GameConst.GRID));
        }


        public string Id { get; }
        public Rectangle Boundary => new Rectangle(new Point(), worldSize);

        public bool FlagIsTouched { get; set; }

        public void Add(IGameObject obj)
        {
            objContainer.Add(obj);
        }

        public void Move(IGameObject obj)
        {
            objContainer.Add(obj);
        }

        public void Remove(IGameObject obj)
        {
            objContainer.Remove(obj);
        }

        public IEnumerable<IGameObject> GetObjects()
        {
            return objContainer.GetObjects();
        }

        public IEnumerable<IGameObject> GetObjects(Rectangle range)
        {
            return objContainer.GetObjects(range);
        }

        public IEnumerable<IGameObject> ScanNearby(Rectangle range)
        {
            int x = range.Left - 64;
            int y = range.Top - 64;
            int width = range.Width + 64;
            int height = range.Height + 64;
            return GetObjects(new Rectangle(x < 0 ? 0 : x, y < 0 ? 0 : y, width, height));
        }

        public IEnumerable<IGameObject> ScanNearby(IGameObject obj)
        {
            return objContainer.GetNearby(obj);
        }

        public Point GetInitialPoint()
        {
            return initialPoint;
        }

        public Point GetRespawnPoint(Point location)
        {
            Point respawnLoc = location;

            foreach (Point point in respawnPoints)
            {
                if (point.X <= location.X && point.X > respawnLoc.X)
                {
                    respawnLoc = point;
                }
            }

            return respawnLoc;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
        public void Update()
        {
            objContainer.Update();
        }
    }
}

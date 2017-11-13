using System.Collections.Generic;
using MelloMario.Collision;
using MelloMario.Theming;
using Microsoft.Xna.Framework;

namespace MelloMario.Containers
{
    class GameWorld2 : IGameWorld
    {
        private readonly Point maxSize;
        private readonly QuadTree<IGameObject> objContainer;
        private readonly Stack<IGameObject> toAdd;
        private readonly Stack<IGameObject> toMove;
        private readonly Stack<IGameObject> toRemove;

        private string id;
        private Point worldSize;
        private Point initialPoint;
        private ISet<Point> respawnPoints;

        public GameWorld2(string id, Point worldSize, Point initialPoint, IEnumerable<Point> respawnPoints)
        {
            this.id = id;
            this.worldSize = worldSize;
            this.initialPoint = new Point(initialPoint.X * GameConst.GRID, initialPoint.Y * GameConst.GRID);
            respawnPoints = new List<Point>();

            foreach (Point respawnPoint in respawnPoints)
            {
                this.respawnPoints.Add(respawnPoint);
            }

            toAdd = new Stack<IGameObject>();
            toMove = new Stack<IGameObject>();
            toRemove = new Stack<IGameObject>();

            objContainer = new QuadTree<IGameObject>(new Rectangle(0, 0, worldSize.X * GameConst.GRID, worldSize.Y * GameConst.GRID));
        }

        public string Id { get { return id; } }
        public Rectangle Boundary { get { return new Rectangle(new Point(), worldSize); } }

        public void Add(IGameObject obj)
        {
            toAdd.Push(obj);
            //objContainer.Add(obj);
        }

        public void Move(IGameObject obj)
        {
            toMove.Push(obj);
            //objContainer.Move(obj);
        }

        public void Remove(IGameObject obj)
        {
            toRemove.Push(obj);
            // objContainer.Remove(obj);
        }

        public IEnumerable<IGameObject> GetRanged(Rectangle range)
        {
            return objContainer.GetObjects(range);
        }

        public IEnumerable<IGameObject> ScanNearby(Rectangle range)
        {
            return GetRanged(range);
        }

        public Point GetInitialPoint()
        {
            return initialPoint;
        }

        public Point GetRespawnPoint(Point location)
        {
            Point respawnLoc = location;

            foreach (Point p in respawnPoints)
            {
                if (p.X <= location.X && p.X > respawnLoc.X)
                {
                    respawnLoc = p;
                }
            }

            return respawnLoc;
        }

        public void Update()
        {
            while (toAdd.Count > 0)
            {
                objContainer.Add(toAdd.Pop());
            }
            while (toMove.Count > 0)
            {
                objContainer.Move(toMove.Pop());
            }
            while (toRemove.Count > 0)
            {
                objContainer.Remove(toRemove.Pop());
            }
        }
    }
}

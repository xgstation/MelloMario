using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelloMario.Collision;
using Microsoft.Xna.Framework;

namespace MelloMario.Containers
{
    class GameWorld2 :IGameWorld
    {
        private readonly int scanRange;
        private readonly int grid;
        private readonly Point maxSize;
        private readonly QuadTree<IGameObject> objContainer;
        private readonly Stack<IGameObject> toAdd;
        private readonly Stack<IGameObject> toMove;
        private readonly Stack<IGameObject> toRemove;

        private string id;
        private Point worldSize;
        private Point initialPoint;
        private ISet<Point> respawnPoints;



        public GameWorld2(string id, Point worldSize, Point initialPoint, IEnumerable<Point> respawnPoints, int grid = 32,
            int scanRange = 24)
        {
            this.id = id;
            this.worldSize = worldSize;
            this.initialPoint = new Point(initialPoint.X * grid, initialPoint.Y * grid);
            respawnPoints = new List<Point>();
            foreach (var respawnPoint in respawnPoints)
            {
                this.respawnPoints.Add(respawnPoint);
            }
            objContainer = new QuadTree<IGameObject>(new Rectangle(0, 0, worldSize.X * grid, worldSize.Y * grid), obj => obj.Boundary);

            toAdd = new Stack<IGameObject>();
            toMove = new Stack<IGameObject>();
            toRemove = new Stack<IGameObject>();

        }
        public string Id { get { return id; } }
        public Rectangle Boundary { get { return new Rectangle(new Point(), worldSize); } }
        public void Add(IGameObject obj)
        {
           objContainer.Add(obj);
        }

        public void Move(IGameObject obj)
        {
            objContainer.DoMove(obj);
        }

        public void Remove(IGameObject obj)
        {
            objContainer.Remove(obj);
        }

        public IEnumerable<IGameObject> GetRanged(Rectangle range)
        {
            return objContainer.GetRanged(range);
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
            //while (toAdd.Count > 0)
            //{
            //    objContainer.Add(toAdd.Pop());
            //}
            //while (toMove.Count > 0)
            //{
            //    objContainer.DoMove(toMove.Pop());
            //}
            //while (toRemove.Count > 0)
            //{
            //    objContainer.Remove(toRemove.Pop());
            //}
        }
    }
}

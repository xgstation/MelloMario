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

        private readonly Stack<IGameObject> toAdd;
        private readonly Stack<IGameObject> toMove;
        private readonly Stack<IGameObject> toRemove;

        private bool flagTouched;
        private Point worldSize;
        private Point initialPoint;
        private ISet<Point> respawnPoints;

        public GameWorld2(string id, Point worldSize, Point initialPoint, IEnumerable<Point> respawnPoints)
        {
            Id = id;
            flagTouched = false;
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


        public string Id { get; }
        public Rectangle Boundary => new Rectangle(new Point(), worldSize);

        public bool FlagTouched
        {
            get => flagTouched;
            set => flagTouched = value;
        }

        public void Add(IGameObject obj) => toAdd.Push(obj);

        public void Move(IGameObject obj) => toMove.Push(obj);

        public void Remove(IGameObject obj) => toRemove.Push(obj);

        public IEnumerable<IGameObject> GetObjects() => objContainer.GetObjects();
        public IEnumerable<IGameObject> GetObjects(Rectangle range) => objContainer.GetObjects(range);

        public IEnumerable<IGameObject> ScanNearby(Rectangle range)
        {
            int x = range.Left - GameConst.SCANRANGE;
            int y = range.Top - GameConst.SCANRANGE;
            int width = range.Width + GameConst.SCANRANGE;
            int height = range.Height + GameConst.SCANRANGE;
            return GetObjects(new Rectangle(x < 0 ? 0 : x, y < 0 ? 0 : y, width, height));
        }

        public Point GetInitialPoint()
        {
            return initialPoint;
        }

        public Point GetRespawnPoint(Point location)
        {
            var respawnLoc = location;

            foreach (var point in respawnPoints)
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

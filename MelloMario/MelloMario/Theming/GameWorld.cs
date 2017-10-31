using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MelloMario
{
    class GameWorld : IGameWorld
    {
        private int grid;
        private Point size;
        private ISet<IGameObject>[,] objects;
        private IDictionary<IGameObject, Point> locations;

        private ISet<IGameObject> toAdd;
        private ISet<IGameObject> toMove;
        private ISet<IGameObject> toRemove;

        private void DoAdd(IGameObject gameObject)
        {
            if (gameObject != null && !locations.ContainsKey(gameObject))
            {
                Point location = new Point(
                    gameObject.Boundary.Center.X / grid,
                    gameObject.Boundary.Center.Y / grid
                );

                if (location.X >= 0 && location.X < size.X && location.Y >= 0 && location.Y < size.Y)
                {
                    locations[gameObject] = location;
                    objects[location.X, location.Y].Add(gameObject);
                }
            }
        }

        private void DoRemove(IGameObject gameObject)
        {
            if (locations.ContainsKey(gameObject))
            {
                Point location = locations[gameObject];

                locations.Remove(gameObject);
                objects[location.X, location.Y].Remove(gameObject);
            }
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

            objects = new HashSet<IGameObject>[size.X, size.Y];
            for (int i = objects.GetLowerBound(0); i <= objects.GetUpperBound(0); ++i)
            {
                for (int j = objects.GetLowerBound(1); j <= objects.GetUpperBound(1); ++j)
                {
                    objects[i, j] = new HashSet<IGameObject>();
                }
            }

            locations = new Dictionary<IGameObject, Point>();
            toAdd = new HashSet<IGameObject>();
            toMove = new HashSet<IGameObject>();
            toRemove = new HashSet<IGameObject>();
        }

        public IEnumerable<IGameObject> ScanObjects()
        {
            for (int i = objects.GetLowerBound(0); i <= objects.GetUpperBound(0); ++i)
            {
                for (int j = objects.GetLowerBound(1); j <= objects.GetUpperBound(1); ++j)
                {
                    // TODO: optimize this
                    IGameObject[] objectList = new IGameObject[objects[i, j].Count];
                    objects[i, j].CopyTo(objectList, 0);

                    foreach (IGameObject obj in objectList)
                    {
                        yield return obj;
                    }
                }
            }
        }

        public IEnumerable<IGameObject> ScanNearbyObjects(IGameObject gameObject)
        {
            if (locations.ContainsKey(gameObject))
            {
                Point location = locations[gameObject];

                for (int i = location.X - 2; i <= location.X + 2; ++i)
                {
                    if (i < objects.GetLowerBound(0) || i > objects.GetUpperBound(0)) continue;

                    for (int j = location.Y - 2; j <= location.Y + 2; ++j)
                    {
                        if (j < objects.GetLowerBound(1) || j > objects.GetUpperBound(1)) continue;

                        // TODO: optimize this
                        IGameObject[] objectList = new IGameObject[objects[i, j].Count];
                        objects[i, j].CopyTo(objectList, 0);
                        foreach (IGameObject obj in objectList)
                        {
                            if (obj != gameObject)
                            {
                                yield return obj;
                            }
                        }
                    }
                }
            }
        }

        public void AddObject(IGameObject gameObject)
        {
            toAdd.Add(gameObject);
        }

        public void MoveObject(IGameObject gameObject)
        {
            toMove.Add(gameObject);
        }

        public void RemoveObject(IGameObject gameObject)
        {
            toRemove.Add(gameObject);
        }

        public void Update()
        {
            foreach (IGameObject obj in toAdd)
            {
                DoAdd(obj);
            }
            toAdd.Clear();

            foreach (IGameObject obj in toMove)
            {
                if (locations.ContainsKey(obj))
                {
                    DoRemove(obj);
                    DoAdd(obj);
                }
            }
            toMove.Clear();

            foreach (IGameObject obj in toRemove)
            {
                DoRemove(obj);
            }
            toRemove.Clear();
        }
    }
}

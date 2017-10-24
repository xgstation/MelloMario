using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MelloMario.Controllers;
using MelloMario.MarioObjects;
using MelloMario.Factories;
using Microsoft.Xna.Framework;

namespace MelloMario
{
    //WIP: This is a GameWorld class in developing for testing purpose.

    class GameWorld2 : IGameWorld
    {
        private int grid;
        private Point size;
        private ISet<IGameObject>[,] objects;
        private IDictionary<IGameObject, Point> locations;
        private GameModel2 gameModel;

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle(0, 0, size.X * grid, size.Y * grid);
            }
        }

        public GameWorld2(int grid, Point size, GameModel2 gameModel)
        {
            this.grid = grid;
            this.size = size;
            this.gameModel = gameModel;
            objects = new HashSet<IGameObject>[size.X, size.Y];
            for (int i = objects.GetLowerBound(0); i <= objects.GetUpperBound(0); ++i)
            {
                for (int j = objects.GetLowerBound(1); j <= objects.GetUpperBound(1); ++j)
                {
                    objects[i, j] = new HashSet<IGameObject>();
                }
            }

            locations = new Dictionary<IGameObject, Point>();
        }

        public GameModel2 GetModel
        {
            get { return gameModel; }
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
            Point location = new Point(
                gameObject.Boundary.Center.X / grid,
                gameObject.Boundary.Center.Y / grid
            );
            locations[gameObject] = location;

            // TODO: handle "out of boundary"
            if (location.X >= 0 && location.X < size.X && location.Y >= 0 && location.Y < size.Y)
            {
                objects[location.X, location.Y].Add(gameObject);
            }
        }

        public void RemoveObject(IGameObject gameObject)
        {
            if (locations.ContainsKey(gameObject))
            {
                Point location = locations[gameObject];
                locations.Remove(gameObject);

                objects[location.X, location.Y].Remove(gameObject);
            }
        }

        public void MoveObject(IGameObject gameObject)
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}

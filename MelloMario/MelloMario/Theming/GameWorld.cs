using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MelloMario.Controllers;
using MelloMario.MarioObjects;
using MelloMario.Factories;
using Microsoft.Xna.Framework;

namespace MelloMario
{
    class GameWorld : IGameWorld
    {
        private int grid;
        private Point size;
        private ISet<IGameObject>[,] objects;
        private IDictionary<IGameObject, Point> locations;

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
        }

        public IEnumerable<IGameObject> ScanObjects()
        {
            for (int i = objects.GetLowerBound(0); i <= objects.GetUpperBound(0); ++i)
            {
                for (int j = objects.GetLowerBound(1); j <= objects.GetUpperBound(1); ++j)
                {
                    foreach (IGameObject obj in objects[i, j])
                    {
                        yield return obj;
                    }
                }
            }
        }

        public IEnumerable<IGameObject> ScanNearbyObjects(IGameObject gameObject)
        {
            Point location = locations[gameObject];

            for (int i = location.X - 2; i <= location.X + 2; ++i)
            {
                if (i < objects.GetLowerBound(0) || i > objects.GetUpperBound(0)) continue;

                for (int j = location.Y - 2; j <= location.Y + 2; ++j)
                {
                    if (j < objects.GetLowerBound(1) || j > objects.GetUpperBound(1)) continue;

                    foreach (IGameObject obj in objects[i, j])
                    {
                        if (obj != gameObject)
                        {
                            yield return obj;
                        }
                    }
                }
            }
        }

        public void AddObject(IGameObject gameObject)
        {
            Point location = new Point(
                (gameObject.Boundary.X + gameObject.Boundary.Width / 2) / grid,
                (gameObject.Boundary.Y + gameObject.Boundary.Height / 2) / grid
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
            Point location = locations[gameObject];
            locations.Remove(gameObject);

            objects[location.X, location.Y].Remove(gameObject);
        }
    }
}

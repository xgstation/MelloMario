namespace MelloMario.LevelGen
{
    #region

    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    #endregion

    internal class StaticGenerator : IGenerator
    {
        private ISet<IGameObject> objects;

        public StaticGenerator()
        {
            objects = new HashSet<IGameObject>();
        }

        public void FeedObjects(IEnumerable<IGameObject> newObjects)
        {
            foreach (IGameObject obj in newObjects)
            {
                // note: we can add them to the world here with no problem
                // but the extra process is to ensure dynamic loading works
                objects.Add(obj);
            }
        }

        public void Request(IWorld world, Rectangle range)
        {
            if (objects != null)
            {
                foreach (IGameObject obj in objects)
                {
                    world.Extend(
                        Math.Max(world.Boundary.Left - obj.Boundary.Left, 0),
                        Math.Max(obj.Boundary.Right - world.Boundary.Right, 0),
                        Math.Max(world.Boundary.Top - obj.Boundary.Top, 0),
                        Math.Max(obj.Boundary.Bottom - world.Boundary.Bottom, 0));
                    world.Add(obj);
                }
                objects = null;
            }
        }
    }
}

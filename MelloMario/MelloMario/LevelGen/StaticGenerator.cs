namespace MelloMario.LevelGen
{
    #region

    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    #endregion

    internal class StaticGenerator : ILevelGenerator
    {
        private IEnumerable<IGameObject> objects;

        public StaticGenerator(IEnumerable<IGameObject> objects)
        {
            this.objects = objects;
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
            else
            {
                // only add objects once, always refuse to extend the world
            }
        }
    }
}

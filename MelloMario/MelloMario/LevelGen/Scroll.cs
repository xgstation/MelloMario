namespace MelloMario.LevelGen
{
    #region

    using System;
    using System.Collections.Generic;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Scroll : IGenerator
    {
        private readonly IList<IGenerator> terrains;

        public Scroll(IList<IGenerator> terrains)
        {
            this.terrains = terrains;
        }

        public void Request(IWorld world, Rectangle range)
        {
            // note: top / buttom are locked

            while (world.Boundary.Left > range.Left - Const.SCAN_RANGE)
            {
                Rectangle subRange = new Rectangle(world.Boundary.Left - Const.GRID, world.Boundary.Top, Const.GRID, world.Boundary.Height);
                terrains[Math.Abs(range.Left / 640) % terrains.Count].Request(world, subRange); // TODO
                world.Extend(Const.GRID, 0, 0, 0);
            }
            while (world.Boundary.Right < range.Right + Const.SCAN_RANGE)
            {
                Rectangle subRange = new Rectangle(world.Boundary.Right, world.Boundary.Top, Const.GRID, world.Boundary.Height);
                terrains[Math.Abs(range.Left / 640) % terrains.Count].Request(world, subRange); // TODO
                world.Extend(0, Const.GRID, 0, 0);
            }
        }
    }
}

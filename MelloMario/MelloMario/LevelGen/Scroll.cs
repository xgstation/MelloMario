namespace MelloMario.LevelGen
{
    #region

    using System.Collections.Generic;
    using MelloMario.Objects.Blocks;
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

            while (world.Boundary.Right < range.Right + Const.SCAN_RANGE)
            {
                terrains[0].Request(world, new Rectangle(world.Boundary.Right, world.Boundary.Top, Const.GRID, world.Boundary.Height)); // TODO
                world.Extend(0, 32, 0, 0);
            }
            while (world.Boundary.Left > range.Left - Const.SCAN_RANGE)
            {
                terrains[0].Request(world, new Rectangle(world.Boundary.Left - Const.GRID, world.Boundary.Top, Const.GRID, world.Boundary.Height)); // TODO
                world.Extend(32, 0, 0, 0);
            }
        }
    }
}

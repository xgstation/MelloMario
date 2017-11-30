namespace MelloMario.LevelGen
{
    #region

    using System;
    using System.Collections.Generic;
    using MelloMario.LevelGen.Terrains;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Scroll : IGenerator
    {
        private readonly IList<IGenerator> terrains;

        public Scroll(IListener<IGameObject> scoreListener)
        {
            this.terrains = new List<IGenerator>
            {
                new Forest(scoreListener),
                new Plain(scoreListener),
                new Sky(scoreListener),
                new Tunnel(scoreListener),
                new Village(scoreListener)
            };
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

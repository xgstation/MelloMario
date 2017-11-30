namespace MelloMario.LevelGen
{
    #region

    using System;
    using System.Collections.Generic;
    using MelloMario.LevelGen.NoiseGenerators;
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

            while (world.Boundary.Left > range.Left - Const.GRID)
            {
                Tuple<int, int> pair = PerlinNoiseGenerator.RandomSplit(23333, world.Boundary.Left / Const.GRID - 1, 16);

                Rectangle subRange = new Rectangle(pair.Item1 * Const.GRID, world.Boundary.Top, (pair.Item2 - pair.Item1) * Const.GRID, world.Boundary.Height);
                terrains[Math.Abs(pair.Item1 * 23333) % terrains.Count].Request(world, subRange); // TODO
                world.Extend((pair.Item2 - pair.Item1) * Const.GRID, 0, 0, 0);
            }
            while (world.Boundary.Right < range.Right + Const.GRID)
            {
                Tuple<int, int> pair = PerlinNoiseGenerator.RandomSplit(23333, world.Boundary.Right / Const.GRID - 1, 16);

                Rectangle subRange = new Rectangle(pair.Item1 * Const.GRID, world.Boundary.Top, (pair.Item2 - pair.Item1) * Const.GRID, world.Boundary.Height);
                terrains[Math.Abs(pair.Item1 * 23333) % terrains.Count].Request(world, subRange); // TODO
                world.Extend(0, (pair.Item2 - pair.Item1) * Const.GRID, 0, 0);
            }
        }
    }
}

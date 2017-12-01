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
            terrains = new List<IGenerator>
            {
                new Forest(scoreListener),
                new Plain(scoreListener),
                new Plain(scoreListener), // more plain terrain // TODO: use weighted list
                new Plateau(scoreListener),
                new Sky(scoreListener),
                new Tunnel(scoreListener)
            };
        }

        public void Request(IWorld world, Rectangle range)
        {
            // note: top / buttom are locked

            while (world.Boundary.Left > range.Left - Const.GRID)
            {
                Tuple<int, int> pair = PerlinNoiseGenerator.RandomSplit(23333, world.Boundary.Left / Const.GRID - 1, 16);

                Rectangle subRange = new Rectangle(
                    pair.Item1 * Const.GRID,
                    world.Boundary.Top,
                    world.Boundary.Left - pair.Item1 * Const.GRID,
                    world.Boundary.Height);
                terrains[Math.Abs(pair.Item1 * 23333) % terrains.Count].Request(world, subRange); // TODO
                world.Extend(subRange.Width, 0, 0, 0);
            }

            while (world.Boundary.Right < range.Right + Const.GRID)
            {
                Tuple<int, int> pair = PerlinNoiseGenerator.RandomSplit(23333, world.Boundary.Right / Const.GRID, 16);

                Rectangle subRange = new Rectangle(
                    world.Boundary.Right,
                    world.Boundary.Top,
                    pair.Item2 * Const.GRID - world.Boundary.Right,
                    world.Boundary.Height);
                terrains[Math.Abs(pair.Item1 * 23333) % terrains.Count].Request(world, subRange); // TODO
                world.Extend(0, subRange.Width, 0, 0);
            }
        }
    }
}

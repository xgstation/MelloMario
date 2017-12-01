namespace MelloMario.LevelGen.Generators
{
    #region

    using System;
    using System.Collections.Generic;
    using MelloMario.LevelGen.Generators.Terrains;
    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Scroll : BaseGenerator
    {
        public Scroll(
            IListener<IGameObject> scoreListener,
            IListener<ISoundable> soundListener) : base(scoreListener, soundListener)
        {
            Children.Add(new Forest(scoreListener, soundListener));
            Children.Add(new Island(scoreListener, soundListener));
            Children.Add(new Plain(scoreListener, soundListener));
            Children.Add(new Plain(scoreListener, soundListener));
            Children.Add(new Plain(scoreListener, soundListener));
            Children.Add(new Plateau(scoreListener, soundListener));
            Children.Add(new Sky(scoreListener, soundListener));
        }

        protected override void OnRequest(IWorld world, Rectangle range)
        {
            // note: top / buttom are locked

            while (world.Boundary.Left > range.Left - Const.GRID)
            {
                Tuple<int, int> pair = PerlinNoiseGenerator.RandomSplit(23333, world.Boundary.Left / Const.GRID - 1, 32);

                Rectangle subRange = new Rectangle(
                    pair.Item1 * Const.GRID,
                    world.Boundary.Top,
                    world.Boundary.Left - pair.Item1 * Const.GRID,
                    world.Boundary.Height);
                RunChild(world, subRange, PerlinNoiseGenerator.Random(23334, pair.Item1));
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
                RunChild(world, subRange, PerlinNoiseGenerator.Random(23334, pair.Item1));
                world.Extend(0, subRange.Width, 0, 0);
            }
        }
    }
}

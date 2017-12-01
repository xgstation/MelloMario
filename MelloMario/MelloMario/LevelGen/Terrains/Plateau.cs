namespace MelloMario.LevelGen.Terrains
{
    #region

    using System;
    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Plateau : IGenerator
    {
        private readonly IListener<IGameObject> listener;

        public Plateau(IListener<IGameObject> listener)
        {
            this.listener = listener;
        }

        public void Request(IWorld world, Rectangle range)
        {
            for (int j = range.Left; j < range.Right; j += Const.GRID)
            {
                int mat = PerlinNoiseGenerator.RandomProp(1001, j / Const.GRID, 10) % 5;

                int height = Math.Min(
                    range.Height / Const.GRID - 6 - PerlinNoiseGenerator.Random(12321, range.Left) % 6,
                    Math.Min(3 + (j - range.Left) / Const.GRID, 3 + (range.Right - Const.GRID - j) / Const.GRID));

                for (int i = 1; i <= height; ++i)
                {
                    if (mat >= 4 || mat >= 2 && i > 3)
                    {
                        world.Add(new Stair(world, new Point(j, range.Bottom - i * Const.GRID), listener));
                    }
                    else
                    {
                        world.Add(new Floor(world, new Point(j, range.Bottom - i * Const.GRID), listener));
                    }
                }
            }
        }
    }
}

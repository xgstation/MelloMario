namespace MelloMario.LevelGen.Generators.Terrains
{
    #region

    using System;
    using MelloMario.Factories;
    using MelloMario.LevelGen.Generators.Objects;
    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Forest : BaseGenerator
    {
        public Forest(IListener<IGameObject> scoreListener, IListener<ISoundable> soundListener) : base(scoreListener, soundListener)
        {
            Children2.Add(new Enemies(scoreListener, soundListener));
        }

        protected override void OnRequest(IWorld world, Rectangle range)
        {
            for (int j = range.Left; j < range.Right; j += Const.GRID)
            {
                int mat = PerlinNoiseGenerator.RandomProp(1001, j / Const.GRID, 20) % 20;

                int height = Math.Min(
                    PerlinNoiseGenerator.Random(1234, j / Const.GRID) % 5
                    + PerlinNoiseGenerator.Random(1234, j / Const.GRID + 1) % 5,
                    Math.Min(3 + 3 * (j - range.Left) / Const.GRID, 3 + 3 * (range.Right - Const.GRID - j) / Const.GRID));

                for (int i = 1; i <= 2 + height; ++i)
                {
                    if (
                        mat < 1
                        || mat < 2 && PerlinNoiseGenerator.Random(1002, j / Const.GRID * 123 + i * 45) % 2 == 0)
                    {
                        AddObject("Stair", world, new Point(j, range.Bottom - i * Const.GRID));
                    }
                    else
                    {
                        AddObject("Floor", world, new Point(j, range.Bottom - i * Const.GRID));
                    }
                }
            }
        }
    }
}

namespace MelloMario.LevelGen.Generators.Terrains
{
    #region

    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Sky : BaseGenerator
    {
        public Sky(IListener<IGameObject> scoreListener, IListener<ISoundable> soundListener) : base(scoreListener, soundListener)
        {
        }

        protected override void OnRequest(IWorld world, Rectangle range)
        {
            for (int j = range.Left; j < range.Right; j += Const.GRID)
            {
                int mat = PerlinNoiseGenerator.RandomProp(1001, j / Const.GRID, 10) % 5;

                int i = PerlinNoiseGenerator.RandomProp(8765, j / Const.GRID, 2) % (range.Height / Const.GRID - 8);

                if (i >= 2)
                {
                    if (mat >= 3)
                    {
                        AddObject("Brick", world, new Point(j, range.Bottom - i * Const.GRID));
                    }
                    if (mat >= 2 || mat >= 1 && PerlinNoiseGenerator.Random(8766, (range.Left + i) / Const.GRID) % 2 != 0)
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

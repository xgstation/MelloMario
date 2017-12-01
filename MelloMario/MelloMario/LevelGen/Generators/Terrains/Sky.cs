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
                int mat = PerlinNoiseGenerator.RandomProp(1001, j / Const.GRID, 20) % 20;

                int i = PerlinNoiseGenerator.RandomProp(8765, j / Const.GRID, 4) % 5 + 2;
                int k = PerlinNoiseGenerator.RandomProp(8765, j / Const.GRID, 4) % 2 - 1;

                if (j == range.Left || j == range.Right - Const.GRID)
                {
                    k = 0;
                }

                if (mat == 4 || mat == 5 || mat == 6)
                {
                    AddObject("Brick", world, new Point(j + k * Const.GRID, range.Bottom - i * Const.GRID));
                }
                else if (mat == 7)
                {
                    AddObject("Question", world, new Point(j + k * Const.GRID, range.Bottom - i * Const.GRID));
                }
                else if (mat < 2 || mat < 4 && PerlinNoiseGenerator.Random(8766, (range.Left + i) / Const.GRID) % 2 != 0)
                {
                    AddObject("Stair", world, new Point(j + k * Const.GRID, range.Bottom - i * Const.GRID));
                }
                else
                {
                    AddObject("Floor", world, new Point(j + k * Const.GRID, range.Bottom - i * Const.GRID));
                }
            }
        }
    }
}

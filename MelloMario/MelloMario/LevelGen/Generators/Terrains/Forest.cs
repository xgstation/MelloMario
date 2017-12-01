namespace MelloMario.LevelGen.Generators.Terrains
{
    #region

    using MelloMario.Factories;
    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Forest : BaseGenerator
    {
        public Forest(IListener<IGameObject> scoreListener, IListener<ISoundable> soundListener) : base(scoreListener, soundListener)
        {
        }

        protected override void OnRequest(IWorld world, Rectangle range)
        {
            for (int j = range.Left; j < range.Right; j += Const.GRID)
            {
                int mat = PerlinNoiseGenerator.RandomProp(1001, j / Const.GRID, 10) % 5;

                for (int i = 1; i <= 2 + PerlinNoiseGenerator.Random(1234, j / Const.GRID) % 5; ++i)
                {
                    if (mat >= 4 || mat >= 3 && PerlinNoiseGenerator.Random(1235, i / Const.GRID) % 2 == 0)
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

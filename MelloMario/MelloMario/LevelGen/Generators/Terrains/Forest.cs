namespace MelloMario.LevelGen.Generators.Terrains
{
    #region

    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Forest : IGenerator
    {
        private readonly IListener<IGameObject> listener;

        public Forest(IListener<IGameObject> listener)
        {
            this.listener = listener;
        }

        public void Request(IWorld world, Rectangle range)
        {
            for (int j = range.Left; j < range.Right; j += Const.GRID)
            {
                int mat = PerlinNoiseGenerator.RandomProp(1001, j / Const.GRID, 10) % 5;

                for (int i = 1; i <= 2 + PerlinNoiseGenerator.Random(1234, j / Const.GRID) % 5; ++i)
                {
                    if (mat >= 4 || mat >= 3 && PerlinNoiseGenerator.Random(1235, i / Const.GRID) % 2 == 0)
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

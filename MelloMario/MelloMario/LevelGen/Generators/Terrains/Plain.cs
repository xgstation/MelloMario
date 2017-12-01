namespace MelloMario.LevelGen.Generators.Terrains
{
    #region

    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Plain : IGenerator
    {
        private readonly IListener<IGameObject> listener;

        public Plain(IListener<IGameObject> listener)
        {
            this.listener = listener;
        }

        public void Request(IWorld world, Rectangle range)
        {
            for (int j = range.Left; j < range.Right; j += Const.GRID)
            {
                int mat = PerlinNoiseGenerator.RandomProp(1001, j / Const.GRID, 10) % 5;

                if (mat >= 4)
                {
                    world.Add(new Stair(world, new Point(j, range.Bottom - 3 * Const.GRID), listener));
                    world.Add(new Stair(world, new Point(j, range.Bottom - 2 * Const.GRID), listener));
                    world.Add(new Stair(world, new Point(j, range.Bottom - 1 * Const.GRID), listener));
                }
                else
                {
                    world.Add(new Floor(world, new Point(j, range.Bottom - 3 * Const.GRID), listener));
                    world.Add(new Floor(world, new Point(j, range.Bottom - 2 * Const.GRID), listener));
                    world.Add(new Floor(world, new Point(j, range.Bottom - 1 * Const.GRID), listener));
                }
            }
        }
    }
}

namespace MelloMario.LevelGen.Generators.Terrains
{
    #region

    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Sky : IGenerator
    {
        private readonly IListener<IGameObject> listener;

        public Sky(IListener<IGameObject> listener)
        {
            this.listener = listener;
        }

        public void Request(IWorld world, Rectangle range)
        {
            for (int j = range.Left; j < range.Right; j += Const.GRID)
            {
                int mat = PerlinNoiseGenerator.RandomProp(1001, j / Const.GRID, 10) % 5;

                int h = PerlinNoiseGenerator.RandomProp(8765, j / Const.GRID, 2) % (range.Height / Const.GRID - 8);

                if (h >= 2)
                {
                    if (mat >= 3)
                    {
                        // TODO
                        //world.Add(
                        //    new Brick(
                        //        world,
                        //        new Point(j, range.Bottom - h * Const.GRID),
                        //        listener));
                    }
                    if (mat >= 2 || mat >= 1 && PerlinNoiseGenerator.Random(8766, (range.Left + h) / Const.GRID) % 2 != 0)
                    {
                        world.Add(
                            new Stair(
                                world,
                                new Point(j, range.Bottom - h * Const.GRID),
                                listener));
                    }
                    else
                    {
                        world.Add(
                            new Floor(
                                world,
                                new Point(j, range.Bottom - h * Const.GRID),
                                listener));
                    }
                }
            }
        }
    }
}

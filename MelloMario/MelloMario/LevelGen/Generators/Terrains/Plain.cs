namespace MelloMario.LevelGen.Generators.Terrains
{
    #region

    using System;
    using MelloMario.LevelGen.Generators.Objects;
    using MelloMario.LevelGen.Generators.Structures;
    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Plain : BaseGenerator
    {
        public Plain(IListener<IGameObject> scoreListener, IListener<ISoundable> soundListener) : base(scoreListener, soundListener)
        {
            Children.Add(new None());
            Children.Add(new None());
            Children.Add(new None());
            Children.Add(new Coins(scoreListener, soundListener));
            Children2.Add(new Backgrounds(scoreListener, soundListener));
            Children2.Add(new Objects.Blocks(scoreListener, soundListener));
            Children2.Add(new Enemies(scoreListener, soundListener));
            Children2.Add(new Enemies(scoreListener, soundListener));
        }

        protected override void OnRequest(IWorld world, Rectangle range)
        {
            for (int j = range.Left; j < range.Right; j += Const.GRID)
            {
                int mat = PerlinNoiseGenerator.RandomProp(1001, j / Const.GRID, 20) % 20;

                for (int i = 1; i <= 3; ++i)
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

                RunChild2(world, new Rectangle(j, range.Bottom - 4 * Const.GRID, Const.GRID, Const.GRID), PerlinNoiseGenerator.Random(23335, j / Const.GRID));
            }

            for (int j = range.Left, k; j < range.Right; j = k)
            {
                Tuple<int, int> pair = PerlinNoiseGenerator.RandomSplit(34567, j / Const.GRID, 8);
                k = Math.Min(pair.Item2 * Const.GRID, range.Right);

                Rectangle subRange = new Rectangle(j, world.Boundary.Top, k - j, world.Boundary.Height - 5 * Const.GRID);
                RunChild(world, subRange, PerlinNoiseGenerator.Random(23335, pair.Item1));
            }
        }
    }
}

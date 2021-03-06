﻿namespace MelloMario.LevelGen.Generators.Terrains
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

    internal class Plateau : BaseGenerator
    {
        public Plateau(IListener<IGameObject> scoreListener, IListener<ISoundable> soundListener) : base(scoreListener, soundListener)
        {
            Children.Add(new Bricks(scoreListener, soundListener));
            Children.Add(new Coins(scoreListener, soundListener));
            Children.Add(new Roof(scoreListener, soundListener));
            Children.Add(new Tunnel(scoreListener, soundListener));
            Children2.Add(new Backgrounds(scoreListener, soundListener));
            Children2.Add(new None());
            Children2.Add(new Enemies(scoreListener, soundListener));
            Children2.Add(new Enemies(scoreListener, soundListener));
        }

        protected override void OnRequest(IWorld world, Rectangle range)
        {
            int baseH = range.Height / Const.GRID - 6 - PerlinNoiseGenerator.Random(12321, range.Left / Const.GRID) % 6;

            for (int j = range.Left; j < range.Right; j += Const.GRID)
            {
                int mat = PerlinNoiseGenerator.RandomProp(1001, j / Const.GRID, 20) % 20;

                int height = Math.Min(
                    baseH,
                    Math.Min(3 + (j - range.Left) / Const.GRID, 3 + (range.Right - Const.GRID - j) / Const.GRID));

                for (int i = 1; i <= height; ++i)
                {
                    if (
                        mat < 1
                        || mat < 2 && PerlinNoiseGenerator.Random(1002, j / Const.GRID) % 2 == 0
                        || mat < 4 && i > 3)
                    {
                        AddObject("Stair", world, new Point(j, range.Bottom - i * Const.GRID));
                    }
                    else
                    {
                        AddObject("Stair", world, new Point(j, range.Bottom - i * Const.GRID));
                    }
                }

                RunChild2(world, new Rectangle(j, range.Bottom - (height + 1) * Const.GRID, Const.GRID, Const.GRID), PerlinNoiseGenerator.Random(23335, j / Const.GRID));
            }

            for (int j = range.Left, k; j < range.Right; j = k)
            {
                Tuple<int, int> pair = PerlinNoiseGenerator.RandomSplit(34567, j / Const.GRID, 8);
                k = Math.Min(pair.Item2 * Const.GRID, range.Right);

                Rectangle subRange = new Rectangle(j, world.Boundary.Top, k - j, world.Boundary.Height - (3 + baseH) * Const.GRID);
                RunChild(world, subRange, PerlinNoiseGenerator.Random(23335, pair.Item1));
            }
        }
    }
}

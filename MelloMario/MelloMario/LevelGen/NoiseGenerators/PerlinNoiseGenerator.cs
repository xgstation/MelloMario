namespace MelloMario.LevelGen.NoiseGenerators
{
    #region

    using System;
    using System.Diagnostics;
    using System.Security.Cryptography;
    using Microsoft.Xna.Framework;

    #endregion

    internal static class PerlinNoiseGenerator
    {
        public static int Random(int seed, int x)
        {
            int x1 = x * 987654321 + seed * 123456789 + 55556666;
            int q = x1 / 127773;
            int r = x1 % 127773;
            return Math.Abs(16807 * r - 2836 * q);
        }

        public static Tuple<int, int> RandomSplit(int seed, int x, int scale)
        {
            // naive implementation
            // TODO: replace with smoothed pseudo-random

            int i = x;
            while (Random(seed, i) % scale != 0)
            {
                i -= 1;
            }

            int j = x + 1;
            while (Random(seed, j) % scale != 0)
            {
                j += 1;
            }

            return new Tuple<int, int>(i, j);
        }

        public static int RandomProp(int seed, int x, int scale)
        {
            // naive implementation
            // TODO: replace with smoothed pseudo-random

            int i = x;
            while (Random(seed, i) % scale != 0)
            {
                i -= 1;
            }

            return Random(seed, i + 1);
        }
    }
}

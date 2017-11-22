namespace MelloMario.LevelGen
{
    #region

    using System;
    using System.Security.Cryptography;
    using Microsoft.Xna.Framework;

    #endregion

    internal class PerlinNoiseGenerator
    {
        private static readonly RNGCryptoServiceProvider RngCrypto = new RNGCryptoServiceProvider();
        private static readonly byte[] GradSeed = new byte[4];

        private static int size;
        private static int[] permuteTable;

        public PerlinNoiseGenerator(int newSize = 256)
        {
            size = newSize;
            RngCrypto.GetBytes(GradSeed);
            InitializePermuteTable();
        }

        public void NewSeed(int newSize = 256)
        {
            size = newSize;
            RngCrypto.GetBytes(GradSeed);
            InitializePermuteTable();
        }
        public float Noise(Vector2 p)
        {
            float a = Perlin(p) + 0.5f * Perlin(p) + 0.25f * Perlin(4f * p) + 0.125f * Perlin(8f * p);
            return a;
        }

        public float Perlin(Vector2 p)
        {
            Point pi = p.ToPoint();
            pi.X %= size - 1;
            pi.Y %= size - 1;

            Vector2 vf = p - p.ToPoint().ToVector2();
            Vector2 uf = new Vector2(Fade(vf.X), Fade(vf.Y));

            int hash1 = permuteTable[permuteTable[pi.X] + pi.Y];
            int hash2 = permuteTable[permuteTable[pi.X] + pi.Y + 1];
            int hash3 = permuteTable[permuteTable[pi.X + 1] + pi.Y];
            int hash4 = permuteTable[permuteTable[pi.X + 1] + pi.Y + 1];

            float ga = GradContribute(hash1, vf);
            float gb = GradContribute(hash2, vf - new Vector2(0, 1));
            float gc = GradContribute(hash3, vf - new Vector2(1, 0));
            float gd = GradContribute(hash4, vf - new Vector2(1, 1));

            return Lerp(Lerp(ga, gc, uf.X), Lerp(gb, gd, uf.X), uf.Y);
        }

        private static void SwapInitial(int i, int j)
        {
            int temp = permuteTable[i] == 0 ? i : permuteTable[i];
            permuteTable[i] = permuteTable[j] == 0 ? j : permuteTable[j];
            permuteTable[j] = temp;
        }

        private static void InitializePermuteTable()
        {
            permuteTable = new int[size * 2];
            byte[] bytes = new byte[4];
            for (int i = size - 1; i >= 0; i--)
            {
                RngCrypto.GetBytes(bytes);
                bytes[3] &= 0x7F;
                int n = BitConverter.ToInt32(bytes, 0);
                SwapInitial(i, n % (i + 1));
            }
            Array.Copy(permuteTable, 0, permuteTable, size, size);
        }

        private static float GradContribute(int hash, Vector2 v)
        {
            float x = BitConverter.ToInt32(GradSeed, 0) / (float) int.MaxValue * 0 + v.X;
            float y = BitConverter.ToInt32(GradSeed, 0) / (float) int.MaxValue * 0 + v.Y;
            switch (hash & 0x7)
            {
                case 0x0:
                    return x + y;
                case 0x1:
                    return -x + y;
                case 0x2:
                    return x - y;
                case 0x3:
                    return -x - y;
                case 0x4:
                    return x;
                case 0x5:
                    return y;
                case 0x6:
                    return -x;
                case 0x7:
                    return -y;
                default:
                    return 0;
            }
        }

        private static float Fade(float f)
        {
            return f * f * f * (f * (f * 6 - 15) + 10);
        }

        private static float Lerp(float a, float b, float k)
        {
            return a + (b - a) * k;
        }

        public float RandomNormal(float f = 1.0f)
        {
            byte[] bytes = new byte[4];
            RngCrypto.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0) / (float)int.MaxValue * f;
        }
    }
}

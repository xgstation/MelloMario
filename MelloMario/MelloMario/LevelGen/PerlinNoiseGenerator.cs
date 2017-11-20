using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;

namespace MelloMario.LevelGen
{
    internal class PerlinNoiseGenerator
    {
        private static readonly RNGCryptoServiceProvider RngCrypto = new RNGCryptoServiceProvider();
        private static readonly byte[] GradSeed = new byte[4];

        private static int Size;
        private static int[] PermuteTable;

        public PerlinNoiseGenerator(int newSize = 256)
        {
            Size = newSize;
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
            pi.X %= (Size - 1);
            pi.Y %= (Size - 1);

            Vector2 vf = p - p.ToPoint().ToVector2();
            Vector2 uf = new Vector2(Fade(vf.X), Fade(vf.Y));

            int hash1 = PermuteTable[PermuteTable[pi.X] + pi.Y];
            int hash2 = PermuteTable[PermuteTable[pi.X] + pi.Y + 1];
            int hash3 = PermuteTable[PermuteTable[pi.X + 1] + pi.Y];
            int hash4 = PermuteTable[PermuteTable[pi.X + 1] + pi.Y + 1];

            float ga = GradContribute(hash1, vf);
            float gb = GradContribute(hash2, vf - new Vector2(0, 1));
            float gc = GradContribute(hash3, vf - new Vector2(1, 0));
            float gd = GradContribute(hash4, vf - new Vector2(1, 1));

            return Lerp(Lerp(ga, gc, uf.X), Lerp(gb, gd, uf.X), uf.Y);
        }

        private static void SwapInitial(int i, int j)
        {
            int temp = PermuteTable[i] == 0 ? i : PermuteTable[i];
            PermuteTable[i] = PermuteTable[j] == 0 ? j : PermuteTable[j];
            PermuteTable[j] = temp;
        }
        private static void InitializePermuteTable()
        {
            PermuteTable = new int[Size * 2];
            byte[] bytes = new byte[4];
            for (int i = Size - 1; i >= 0; i--)
            {
                RngCrypto.GetBytes(bytes);
                bytes[3] &= 0x7F;
                int n = BitConverter.ToInt32(bytes, 0);
                SwapInitial(i, n % (i + 1));
            }
            Array.Copy(PermuteTable, 0, PermuteTable, 256, 256);
        }

        private static float GradContribute(int hash, Vector2 v)
        {

            float x = BitConverter.ToInt32(GradSeed, 0) / (float)int.MaxValue * 0 + v.X;
            float y = BitConverter.ToInt32(GradSeed, 0) / (float)int.MaxValue * 0 + v.Y;
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
    }
}
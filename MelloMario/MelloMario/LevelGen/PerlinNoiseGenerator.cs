using System;
using System.Security.Cryptography;

namespace MelloMario.LevelGen
{
    //Work In Progress
    internal class PerlinNoiseGenerator
    {
        private static readonly RNGCryptoServiceProvider RngCrypto = new RNGCryptoServiceProvider();
        private static int[] table;

        public void Initialize()
        {
            table = new int[256];
            for (int i = 0; i < 256; i++)
            {
                var bytes = new byte[4];
                RngCrypto.GetBytes(bytes);
                table[i] = BitConverter.ToInt32(bytes, 0) % 256;
            }
        }

        public float Perlin(params float[] p)
        {
            return 0;
        }
    }
}
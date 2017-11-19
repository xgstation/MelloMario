using System;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;

namespace MelloMario.LevelGen
{
    //Work In Progress
    internal class PerlinNoiseGenerator
    {
        private static readonly RNGCryptoServiceProvider RngCrypto = new RNGCryptoServiceProvider();
        private static int Size;

        private static int[] PermuteTable =
        {
            151, 160, 137, 91, 90, 15,  131, 13, 201, 95, 96, 53, 194, 233, 7, 225, 140, 36, 103, 30, 69, 142, 8, 99, 37, 240, 21, 10, 23,
            190, 6, 148, 247, 120, 234, 75, 0, 26, 197, 62, 94, 252, 219, 203, 117, 35, 11, 32, 57, 177, 33, 88, 237,
            149, 56, 87, 174, 20, 125, 136, 171, 168, 68, 175, 74, 165, 71, 134, 139, 48, 27, 166, 77, 146, 158, 231,
            83, 111, 229, 122, 60, 211, 133, 230, 220, 105, 92, 41, 55, 46, 245, 40, 244, 102, 143, 54, 65, 25, 63, 161,
            1, 216, 80, 73, 209, 76, 132, 187, 208, 89, 18, 169, 200, 196, 135, 130, 116, 188, 159, 86, 164, 100, 109,
            198, 173, 186, 3, 64, 52, 217, 226, 250, 124, 123, 5, 202, 38, 147, 118, 126, 255, 82, 85, 212, 207, 206,
            59, 227, 47, 16, 58, 17, 182, 189, 28, 42, 223, 183, 170, 213, 119, 248, 152, 2, 44, 154, 163, 70, 221, 153,
            101, 155, 167, 43, 172, 9, 129, 22, 39, 253, 19, 98, 108, 110, 79, 113, 224, 232, 178, 185, 112, 104, 218,
            246, 97, 228, 251, 34, 242, 193, 238, 210, 144, 12, 191, 179, 162, 241, 81, 51, 145, 235, 249, 14, 239, 107,
            49, 192, 214, 31, 181, 199, 106, 157, 184, 84, 204, 176, 115, 121, 50, 45, 127, 4, 150, 254, 138, 236, 205,
            93, 222, 114, 67, 29, 24, 72, 243, 141, 128, 195, 78, 66, 215, 61, 156, 180,151, 160, 137, 91, 90, 15,  131, 13, 201, 95, 96, 53, 194, 233, 7, 225, 140, 36, 103, 30, 69, 142, 8, 99, 37, 240, 21, 10, 23,
            190, 6, 148, 247, 120, 234, 75, 0, 26, 197, 62, 94, 252, 219, 203, 117, 35, 11, 32, 57, 177, 33, 88, 237,
            149, 56, 87, 174, 20, 125, 136, 171, 168, 68, 175, 74, 165, 71, 134, 139, 48, 27, 166, 77, 146, 158, 231,
            83, 111, 229, 122, 60, 211, 133, 230, 220, 105, 92, 41, 55, 46, 245, 40, 244, 102, 143, 54, 65, 25, 63, 161,
            1, 216, 80, 73, 209, 76, 132, 187, 208, 89, 18, 169, 200, 196, 135, 130, 116, 188, 159, 86, 164, 100, 109,
            198, 173, 186, 3, 64, 52, 217, 226, 250, 124, 123, 5, 202, 38, 147, 118, 126, 255, 82, 85, 212, 207, 206,
            59, 227, 47, 16, 58, 17, 182, 189, 28, 42, 223, 183, 170, 213, 119, 248, 152, 2, 44, 154, 163, 70, 221, 153,
            101, 155, 167, 43, 172, 9, 129, 22, 39, 253, 19, 98, 108, 110, 79, 113, 224, 232, 178, 185, 112, 104, 218,
            246, 97, 228, 251, 34, 242, 193, 238, 210, 144, 12, 191, 179, 162, 241, 81, 51, 145, 235, 249, 14, 239, 107,
            49, 192, 214, 31, 181, 199, 106, 157, 184, 84, 204, 176, 115, 121, 50, 45, 127, 4, 150, 254, 138, 236, 205,
            93, 222, 114, 67, 29, 24, 72, 243, 141, 128, 195, 78, 66, 215, 61, 156, 180
        };

        public PerlinNoiseGenerator(int size)
        {
            Size = size;
            //InitializeNormal();
        }
        
        public void InitializeNormal()
        {
            PermuteTable = new int[Size * 2];
            for (int i = 0; i < Size * 2; i++)
            {
                byte[] bytes = new byte[4];
                RngCrypto.GetBytes(bytes);
                PermuteTable[i] = BitConverter.ToInt32(bytes, 0) % Size;
            }
        }

        public float Perlin(Vector2 loc)
        {
            float x = loc.X;
            float y = loc.Y;
            int vx = (int)x % Size;
            int vy = (int)y % Size;
            float rx = x - (int)x;
            float ry = y - (int)y;
            float fx = Fade(rx);
            float fy = Fade(ry);

            int hash1 = PermuteTable[PermuteTable[vx] + vy];
            int hash2 = PermuteTable[PermuteTable[vx] + vy + 1];
            int hash3 = PermuteTable[PermuteTable[vx + 1] + vy];
            int hash4 = PermuteTable[PermuteTable[vx + 1] + vy + 1];


            float u = Lerp(Grad(hash1, rx, ry), Grad(hash3, rx - 1, ry), fx);
            float v = Lerp(Grad(hash2, rx, ry - 1), Grad(hash4, rx, ry), fy);

            return (u + v) / 2;
        }

        private static float Grad(int hash, float x, float y)
        {
            switch (hash & 0x3)
            {
                case 0x0:
                    return x + y;
                case 0x1:
                    return -x + y;
                case 0x2:
                    return x - y;
                case 0x3:
                    return -x - y;
                default: return 0;
            }
        }

        private static float Fade(float f)
        {
            return (float)(6 * Math.Pow(f, 5) - 15 * Math.Pow(f, 4) + 10 * Math.Pow(f, 3));
        }

        private static float Lerp(float a, float b, float k)
        {
            return a + k * (b - a);
        }
    }
}
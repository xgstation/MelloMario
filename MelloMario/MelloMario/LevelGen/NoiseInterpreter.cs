using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Work in progress
namespace MelloMario.LevelGen
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class NoiseInterpreter
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly SpriteBatch spriteBatch;
        public NoiseInterpreter(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            graphicsDevice = spriteBatch.GraphicsDevice;
            Produce();
        }
        private Texture2D cellTexture2D;

        private readonly PerlinNoiseGenerator perlin = new PerlinNoiseGenerator(5120);
        private void Produce()
        {
            cellTexture2D = new Texture2D(graphicsDevice, 2560, 600);
            Color[] originData = new Color[2560 * 600];
            Color[] cellData = new Color[2560 * 600];
            Color c = new Color(80, 80, 80);
            for (int r = 0; r < 50; r++)
            {
                perlin.NewSeed((int)(5120 * (r + 1) / 50f));
                for (int x = 1; x < 2560; x++)
                {
                    float p = perlin.Noise(new Vector2(x + r * 10, 0) * 0.0025f);
                    int i = (int)(p * 200f + r);
                    i = i > 0 ? i : -i;
                    for (int j = i; j > 0; j--)
                    {
                        originData[x + (600 - j) * 2560] = c;
                    }

                }
            }
            for (int x = 0; x < 2560; x += 16)
            {
                float seed = Math.Abs(perlin.RandomNormal());
                if (seed < 0.1f)
                {
                    //x += 16;
                    if (seed < 0.09f)
                    {
                        //x += 16;
                    }

                }
                for (int y = 0; y < 600; y += 16)
                {
                    if (originData[x + y * 2560].Equals(c))
                    {
                        for (int n = y; n < 600; n += 16)
                        {
                            if (n + 15 >= 600)
                            {
                                break;
                            }

                            float R = perlin.Perlin(new Vector2(x, n) * 0.009f);
                            float G = perlin.Perlin(new Vector2(x, n) * 0.01f);
                            float B = perlin.Perlin(new Vector2(x, n) * 0.012f);
                            Vector3 colorVector3 = new Vector3(R, G, B);
                            colorVector3 = colorVector3 * 2f;
                            colorVector3.Normalize();
                            Color color = new Color(colorVector3);
                            RenderCell(ref cellData, x, n, color);
                        }
                    }
                }

            }
            cellTexture2D.SetData(cellData);
        }

        private static void RenderCell(ref Color[] data, int x, int y, Color color)
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    data[x + i + (y + j) * 2560] = color;
                }
            }
        }

        public void Draw()
        {
            spriteBatch.Draw(cellTexture2D, Vector2.Zero, Color.White);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.LevelGen.NoiseGenerators
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class NoiseConverter : IDisposable
    {
        private const int REPEAT_I = 50;
        private const int REPEATSHIFT_I = 10;
        private const float NOISESCALE_F = 0.005f;
        private const int GRID_I = 16;
        private int width, height;
        private Texture2D debugTexture2D;
        private readonly SpriteBatch spriteBatch;
        private readonly IDictionary<int, int> oneDimPerlin;
        public void SetSize(int newWidth, int newHeight)
        {
            width = newWidth * 2;
            height = newHeight * 2;
        }

        public IDictionary<int, int> GetData()
        {
            FeedData();
            return oneDimPerlin;
        }


        public NoiseConverter(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            oneDimPerlin = new Dictionary<int, int>();
        }

        private void FeedData()
        {
            PerlinNoiseGenerator perlin = new PerlinNoiseGenerator(Math.Max(width * GRID_I, height * GRID_I));
            for (int repeat = 1; repeat <= REPEAT_I; repeat++)
            {
                for (int x = 0; x < width * GRID_I; x++)
                {
                    float oneDimNoise = perlin.Noise(new Vector2(x + repeat * REPEATSHIFT_I, 0) * NOISESCALE_F);
                    int amplitude = Math.Abs((int) (oneDimNoise * height * GRID_I * 1.5f));
                    amplitude = MathHelper.Clamp(amplitude, 400, 800);
                    if (oneDimPerlin.ContainsKey(x))
                    {
                        oneDimPerlin[x] = amplitude;
                    }
                    else
                    {
                        oneDimPerlin.Add(x, amplitude);
                    }
                }
            }

        }

        public void DebugFill()
        {
            try
            {

                DebugFillColor();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private void DebugFillColor()
        {
            Color[] cellData = new Color[width * height * GRID_I * GRID_I];
            PerlinNoiseGenerator perlinColor = new PerlinNoiseGenerator(width * GRID_I);
            for (int x = 0; x < GRID_I * width; x += GRID_I)
            {
                //if (seed < 0.1f)
                //{
                //    x += GRID_I;
                //    if (seed < 0.09f)
                //    {
                //        x += GRID_I;
                //    }
                //}
                for (int y = oneDimPerlin[x]; y < GRID_I * height; y += GRID_I)
                {
                    for (int n = y; n < height * GRID_I; n += GRID_I)
                    {
                        if (n + GRID_I - 1 >= height * GRID_I)
                        {
                            break;
                        }

                        float R = perlinColor.Perlin(new Vector2(x, n) * 0.009f) * 2f;
                        float G = perlinColor.Perlin(new Vector2(x, n) * 0.01f) * 2f;
                        float B = perlinColor.Perlin(new Vector2(x, n) * 0.012f) * 2f;

                        Vector3 colorVector3 = new Vector3(R, G, B);
                        colorVector3.Normalize();

                        Color color = new Color(colorVector3);

                        if (true)
                        {
                            RenderCell(ref cellData, x, n, color);
                        }
                    }
                }
            }
            debugTexture2D = new Texture2D(spriteBatch.GraphicsDevice, width * GRID_I, height * GRID_I);
            debugTexture2D.SetData(cellData);
        }
        private void RenderCell(ref Color[] cellData, int x, int y, Color color)
        {
            for (int i = 0; i < GRID_I - 1; i++)
            {
                for (int j = 0; j < GRID_I - 1; j++)
                {
                    cellData[x + i + (y + j) * width * GRID_I] = color;
                }
            }
        }

        public void Draw()
        {
            spriteBatch.Draw(debugTexture2D, Vector2.Zero, Color.White);
        }

        public void Dispose()
        {
            debugTexture2D.Dispose();
        }
    }

}

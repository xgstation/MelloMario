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
            Produce(perlinColor, ref cellTexture2D);
            Produce(perlinColor, ref cellTexture2D2, 1);
            Produce(perlinColor, ref cellTexture2D3, 2);
        }
        private readonly Texture2D cellTexture2D;
        private readonly Texture2D cellTexture2D2;
        private readonly Texture2D cellTexture2D3;
        private const int GRID_I = 4;

        private readonly PerlinNoiseGenerator perlinTerrian = new PerlinNoiseGenerator(5120);
        private readonly PerlinNoiseGenerator perlinColor = new PerlinNoiseGenerator(2560);
        private void Produce(PerlinNoiseGenerator perlinColor, ref Texture2D texture2D, int parameter = 0)
        {
            texture2D = new Texture2D(graphicsDevice, 2560, 500);
            Color[] originData = new Color[2560 * 500];
            Color[] cellData = new Color[2560 * 500];
            Color c = new Color(80, 80, 80);
            for (int r = 0; r < 50; r++)
            {
                perlinTerrian.NewSeed((int)(5120 * (r + 1) / 50f));
                for (int x = 1; x < 2560; x++)
                {
                    float p = perlinTerrian.Noise(new Vector2(x + r * 10, 0) * 0.0025f, parameter);
                    int i = (int)(p * 550f + r);
                    i = i > 0 ? i : -i;
                    for (int j = i; j > 0; j--)
                    {
                        var index = x + (500 - j) * 2560;
                        if (index > 0 && index < 2560 * 500)
                        {
                            originData[index] = c;
                        }
                    }

                }
            }
            for (int x = 0; x < 2560; x += GRID_I)
            {
                float seed = Math.Abs(perlinTerrian.RandomNormal());
                if (seed < 0.1f)
                {
                    //x += GRID_I;
                    if (seed < 0.09f)
                    {
                        //x += GRID_I;
                    }

                }
                for (int y = 0; y < 500; y += GRID_I)
                {
                    if (originData[x + y * 2560].Equals(c))
                    {
                        for (int n = y; n < 500; n += GRID_I)
                        {
                            if (n + GRID_I - 1 >= 500)
                            {
                                break;
                            }

                            float R = perlinColor.Perlin(new Vector2(x, n) * 0.009f, parameter) * 2f;
                            float G = perlinColor.Perlin(new Vector2(x, n) * 0.01f, parameter) * 2f;
                            float B = perlinColor.Perlin(new Vector2(x, n) * 0.012f, parameter) * 2f * 0;

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

            }
            texture2D.SetData(cellData);
        }
        
        private static void RenderCell(ref Color[] data, int x, int y, Color color)
        {
            for (int i = 0; i < GRID_I -1; i++)
            {
                for (int j = 0; j < GRID_I -1; j++)
                {
                    data[x + i + (y + j) * 2560] = color;
                }
            }
        }

        public void Draw()
        {
            spriteBatch.Draw(cellTexture2D, Vector2.Zero, Color.White);
            spriteBatch.Draw(cellTexture2D2, new Vector2(0f, 500f), Color.White);
            spriteBatch.Draw(cellTexture2D3, new Vector2(0f, 1000f), Color.White);
        }
    }
}

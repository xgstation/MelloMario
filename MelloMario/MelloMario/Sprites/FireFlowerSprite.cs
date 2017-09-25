using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Sprites
{
    public class fireFlowerSprite : ISprite
    {
        public Texture2D flower { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int frames;
        private int totalFrames;
        private float elapsed;
        private float delay = 250f;
        private Vector2 pos;

        public fireFlowerSprite(Texture2D fireFlower, int InputRows, int InputColumns)
        {
            flower = fireFlower;
            Rows = InputRows;
            Columns = InputColumns;
            frames = 0;
            totalFrames = Rows * Columns;

        }

        public void Update(GameTime gameTime)
        {


            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                frames++;
                if (frames == totalFrames)
                {
                    frames = 0;
                }
                elapsed = 0;
            }

        }
        public void Draw(SpriteBatch itemSprite, Vector2 location)
        {
            pos = location;
            int Width = flower.Width / Columns;
            int Height = flower.Height / Rows;
            int R = frames / Columns;
            int C = frames % Columns;
            Rectangle Last = new Rectangle((int)pos.X, (int)pos.Y, Width, Height);
            Rectangle First = new Rectangle(Width * C, R * Height, Width, Height);
            itemSprite.Begin();
            itemSprite.Draw(flower, Last, First, Color.White);
            itemSprite.End();
        }
    }
}

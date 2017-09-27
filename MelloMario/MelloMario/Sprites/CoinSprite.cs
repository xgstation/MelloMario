using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Sprites
{
    public class CoinSprite : ISprite
    {
        public Texture2D coin { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int frames;
        private int totalFrames;
        private float elapsed;
        private float delay = 250f;
        private Vector2 pos;

        public CoinSprite(Texture2D pic, int InputRows, int InputColumns)
        {
            coin = pic;
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
            int Width = coin.Width / Columns;
            int Height = coin.Height / Rows;
            int R = frames / Columns;
            int C = frames % Columns;
            Rectangle Last = new Rectangle((int)pos.X, (int)pos.Y, Width, Height);
            Rectangle First = new Rectangle(Width * C, R * Height, Width, Height);
            
            itemSprite.Draw(coin, Last, First, Color.White);
         
        }
    }
}

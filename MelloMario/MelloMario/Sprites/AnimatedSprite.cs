using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace MelloMario.Sprites
{
    class AnimatedSprite : ISprite
    {
        public Texture2D texture;
        Color defaultColor;
        private int rows;
        private int columns;
        private int frames;
        private int totalFrames;
        private float elapsed;
        private float delay = 250f;

        public AnimatedSprite(Texture2D texture, int rows, int columns)
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            frames = 0;
            totalFrames = rows * columns;
            defaultColor = Color.White;

        }

        public void Update(GameTime time)
        {
            //do update after moving frame logic
            elapsed += (float)time.ElapsedGameTime.TotalMilliseconds;
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

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int Width = texture.Width / columns;
            int Height = texture.Height / rows;
            int R = frames / columns;
            int C = frames % columns;
            Rectangle Last = new Rectangle((int)location.X, (int)location.Y, Width, Height);
            Rectangle First = new Rectangle(Width * C, R * Height, Width, Height);
            spriteBatch.Draw(texture, Last, First, Color.White);
        }
    }
}

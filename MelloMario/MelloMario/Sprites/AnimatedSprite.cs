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
        Texture2D texture;
        Color defaultColor;
        private int rows;
        private int columns;
        private int frame;
        private int beginFrame;
        private int endFrame;
        private float elapsed;
        private float delay = 250f;

        public AnimatedSprite(Texture2D newTexture, int rows, int columns)
        {
            texture = newTexture;
            defaultColor = Color.White;
            this.rows = rows;
            this.columns = columns;
            frame = 0;
            beginFrame = 0;
            endFrame = rows * columns;
        }

        public AnimatedSprite(Texture2D newTexture, int rows, int columns, int begin, int end)
        {
            texture = newTexture;
            defaultColor = Color.White;
            this.rows = rows;
            this.columns = columns;
            frame = 0;
            beginFrame = begin;
            endFrame = end;
        }

        public void Update(GameTime time)
        {
            //do update after moving frame logic
            elapsed += (float)time.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                frame++;
                if (frame == endFrame)
                {
                    frame = beginFrame;
                }
                elapsed = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int Width = texture.Width / columns;
            int Height = texture.Height / rows;
            int R = frame / columns;
            int C = frame % columns;
            Rectangle Last = new Rectangle((int)location.X, (int)location.Y, Width, Height);
            Rectangle First = new Rectangle(Width * C, R * Height, Width, Height);
            spriteBatch.Draw(texture, Last, First, Color.White);
        }
    }
}

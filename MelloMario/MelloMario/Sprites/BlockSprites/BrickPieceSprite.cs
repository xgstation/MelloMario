using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Sprites.BlockSprites
{
    class BrickPieceSprite : ISprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Part part;

        private int x;
        private int y;

        private int frames;
        private float elapsed;
        private float offsetX;
        private float offsetY;

        private void UpdateOffset(GameTime time)
        {
            elapsed += (float)time.ElapsedGameTime.TotalMilliseconds;
            frames += 1;

            switch (part)
            {
                case Part.LeftBottom:
                    offsetX = -elapsed / 5f + 4;
                    offsetY = (float)Math.Pow(elapsed / 20f, 2f) - elapsed + 16f;
                    break;
                case Part.LeftTop:
                    offsetX = -elapsed / 5f + 4;
                    offsetY = (float)Math.Pow(elapsed / 19f, 2f) - elapsed + 32f;
                    break;
                case Part.RightBottom:
                    offsetX = elapsed / 5f + 12;
                    offsetY = (float)Math.Pow(elapsed / 20f, 2f) - elapsed + 16f;
                    break;
                case Part.RightTop:
                    offsetX = elapsed / 5f + 12;
                    offsetY = (float)Math.Pow(elapsed / 19f, 2f) - elapsed + 32f;
                    break;
            }

        }

        public enum Part
        {
            LeftTop, LeftBottom, RightTop, RightBottom
        }

        public Point PixelSize
        {
            get
            {
                return new Point(16, 16);
            }
        }

        public BrickPieceSprite(SpriteBatch spriteBatch, Texture2D texture, Part part, int x, int y)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.part = part;
            this.x = x;
            this.y = y;

            frames = 0;
            elapsed = 0;
            offsetX = 0f;
            offsetY = 0f;
        }

        public void Draw(GameTime time, Rectangle destination, ZIndex zIndex)
        {
            if (zIndex == ZIndex.front)
            {
                UpdateOffset(time);

                Rectangle newDestination = new Rectangle(destination.Left + (int)offsetX, destination.Top + (int)offsetY, destination.Width, destination.Height);
                Rectangle source = new Rectangle(texture.Width * x / 2, texture.Height * y / 2, texture.Width / 2, texture.Height / 2);
                spriteBatch.Draw(texture, newDestination, source, Color.White);
            }
        }
    }
}

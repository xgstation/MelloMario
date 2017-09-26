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
        public enum Part
        {
            LeftTop, LeftBottom, RightTop, RightBottom
        }
        private Part part;
        private int frames;
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int posX, posY;
        private float elapsed;
        private Vector2 offset;
        public Texture2D texture { get; set; }
        public BrickPieceSprite(Texture2D texture, int Rows, int Columns, int posX, int posY, Part part)
        {
            this.texture = texture;
            offset = new Vector2(0f, 0f);
            this.part = part;
            this.Rows = Rows;
            this.posX = posX;
            this.posY = posY;
            this.Columns = Columns;

        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int Width = texture.Width / Columns;
            int Height = texture.Height / Rows;
            int R = frames / Columns;
            int C = frames % Columns;
            Rectangle Last = new Rectangle((int)(location.X + offset.X), (int)(location.Y + offset.Y), Width*2, Height*2);
            Rectangle First = new Rectangle(Width * posX, posY * Height, Width, Height);
            spriteBatch.Draw(texture, Last, First, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            frames++;
            switch (part)
            {
                case Part.LeftBottom:
                    offset.X = -4 * elapsed;
                    offset.Y = (float)Math.Pow(offset.X, 2) - offset.X + 8f;
                    break;
                case Part.LeftTop:
                    offset.X = -8 * elapsed;
                    offset.Y = (float)Math.Pow(offset.X, 2) - offset.X + 16f;
                    break;
                case Part.RightBottom:
                    offset.X = 4 * elapsed;
                    offset.Y = (float)Math.Pow(offset.X, 2) - offset.X + 8f;
                    break;
                case Part.RightTop:
                    offset.X = 8 * elapsed;
                    offset.Y = (float)Math.Pow(offset.X, 2) - offset.X + 16f;
                    break;
            }
            
        }
    }
}

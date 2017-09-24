using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Sprites
{
    class BrickSprite : ISprite
    {
        private const int totalFrame = 5;
        private int currentFrame = 0;
        private int Columns = 0;

        private Texture2D Texture;

        public BrickSprite()
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            currentPositionX = 0;
        }

        public int TotalFrame()
        {
            return totalFrame;
        }

        public void Update(GameTime time) { }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns; //horizontal Cell
            int height = Texture.Height / Rows; //vertical Cell
            int row = (int)((float)currentFrame / (float)Columns); //row cell frame index
            int column = currentFrame % Columns; //column cell index
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height); //pick cell
            Rectangle destinationRectangle = new Rectangle(currentPositionX, (int)location.Y / 2, width * 2, height * 2);
            if (currentPositionX == (int)location.X)
            {
                currentPositionX = 0;
            }
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}

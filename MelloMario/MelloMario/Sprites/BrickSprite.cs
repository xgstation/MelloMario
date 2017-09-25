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
        private int Rows;
        private int Columns;

        private int totalFrames;
        private int currentFrame;
        private int currentPositionX;

        private Texture2D Texture;

        public BrickSprite(int rows, int columns, int posX)
        {
            // TODO: Texture = texture;
            Rows = rows;
            Columns = columns;

            currentFrame = 0;
            currentPositionX = posX;
            totalFrames = Rows * Columns;
        }

        public int TotalFrames()
        {
            return totalFrames;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Sprites.BlockSprites
{
    class BlockAnimatedSprite
    {
        private Texture2D texture;
        int row, col;

        public BlockAnimatedSprite(Texture2D texture, int row, int col)
        {
            this.texture = texture;
            this.row = row;
            this.col = col;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            Rectangle area = new Rectangle(col * 16, row * 16, 16, 16);
            Rectangle destRect = new Rectangle(new Point((int)location.X, (int)location.Y), new Point(32));
            spriteBatch.Draw(texture, destRect, area, Color.White);
        }

        public void Update(GameTime game)
        {
            //do nothing
        }
    }
}

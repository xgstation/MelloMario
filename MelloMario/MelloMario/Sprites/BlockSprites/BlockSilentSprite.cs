using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Sprites.BlockSprites
{
    class BlockSilentSprite : ISprite
    {
        private Texture2D texture;
        int row, col, w, l;

        public BlockSilentSprite(Texture2D texture,int row, int col, int w, int l)
        {
            this.texture = texture;
            this.row = row;
            this.col = col;
            this.w = w;
            this.l = l;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            Rectangle area = new Rectangle(col*16,row*16,w,l);
            Rectangle destRect = new Rectangle(new Point((int)location.X, (int)location.Y),new Point(w * 2));
            spriteBatch.Draw(texture, destRect, area, Color.White);
        }

        public void Update(GameTime game)
        {
            //do nothing
        }
    }
}

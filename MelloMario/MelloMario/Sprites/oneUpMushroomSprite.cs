using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Sprites
{
    public class oneUpshroomSprite : ISprite
    {
        public Texture2D mushroom { get; set; }
        private Vector2 pos;
        public oneUpshroomSprite(Texture2D pic)
        {
            mushroom = pic;
        }

        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch itemSprite, Vector2 location)
        {
            pos = location;
            itemSprite.Begin();
            itemSprite.Draw(mushroom, pos, Color.White);
            itemSprite.End();
        }
    }
}

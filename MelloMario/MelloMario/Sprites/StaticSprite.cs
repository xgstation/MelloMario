using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Sprites
{
    class StaticSprite : ISprite
    {
        public Texture2D texture { get ; set; }
        Color default_color;

        public StaticSprite(Texture2D newTexture)
        {
            texture = newTexture;
            default_color = Color.White;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteBatch.Draw(texture,location,default_color);
        }

        public void Update(GameTime game)
        {

        }
    }
}

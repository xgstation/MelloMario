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
        Texture2D texture;
        Color defaultColor;

        public StaticSprite(Texture2D newTexture)
        {
            texture = newTexture;
            defaultColor = Color.White;
        }

        public void Update(GameTime time)
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteBatch.Draw(texture,location,defaultColor);
        }
    }
}

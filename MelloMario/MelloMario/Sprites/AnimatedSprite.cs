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
        //add motion later
        public Texture2D texture { get; set; }
        Color defaultColor;

        public AnimatedSprite(Texture2D texture)
        {
            this.texture = texture;
            defaultColor = Color.White;

        }

        public void Update(GameTime time)
        {
            //do update after moving frame logic
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            //do moving frame logic later
            spriteBatch.Draw(texture,location,defaultColor);
            
        }
    }
}

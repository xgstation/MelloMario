using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario
{
    class SpriteFactory : ISpriteFactory
        
    {
        
    
        public SpriteFactory()
        {        
        }
        public ISprite createSprite(string textureName, bool Static,ContentManager content)
        {
            
   
            Texture2D spriteTexture = content.Load<Texture2D>(textureName);
            ISprite sprite;
            //static
            if (Static)
            {
               sprite = new StaticSprite(spriteTexture);
            }
            //animated
            else
            {
                //add additional parameters when motion is involved
                sprite = new AnimatedSprite(spriteTexture);
            }
            return sprite;
        }
    }
}

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
    class StaticSprite : BaseSprite
    {
        protected override void OnAnimate(GameTime time)
        {
            // Do nothing
        }

        public StaticSprite(SpriteBatch spriteBatch, Texture2D texture, ZIndex activeZIndex = ZIndex.main) : base(
            spriteBatch, texture, new Point(), new Point(texture.Width, texture.Height), activeZIndex
        )
        {
        }
    }
}

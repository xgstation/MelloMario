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
    class SlicedSprite : BaseSprite
    {
        protected override void OnAnimate(GameTime time)
        {
            // Do nothing
        }

        public SlicedSprite(SpriteBatch spriteBatch, Texture2D texture, int columns, int rows, int x, int y, int width, int height, ZIndex activeZIndex = ZIndex.main) : base(
            spriteBatch, texture, new Point(texture.Width * x / columns, texture.Height * y / rows), new Point(texture.Width * width / columns, texture.Height * height / rows), activeZIndex
        )
       {
        }
    }
}

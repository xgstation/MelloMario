using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Sprites
{
    class SplashSprite : BaseSprite
    {
        private Texture2D screen;
        public SplashSprite(SpriteBatch spriteBatch, string text, SpriteFont font, Point size, ZIndex zIndex) : base(spriteBatch, size, zIndex)
        {
            screen = new Texture2D(spriteBatch.GraphicsDevice, size.X, size.Y);
            Color[] data = new Color[size.X * size.Y];
            for(int i=0;i< data.Length;i++)
            {
                data[i] = Color.Black;
            }
            screen.SetData(data);
        }

        protected override void OnDraw(int time, Rectangle destination)
        {
            spriteBatch.Draw(screen, new Vector2(0, 0));
        }
    }
}
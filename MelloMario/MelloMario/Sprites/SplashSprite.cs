using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Sprites
{
    class SplashSprite : BaseSprite
    {
        private Texture2D screen;

        public SplashSprite(SpriteBatch spriteBatch, Point size, ZIndex zIndex) : base(spriteBatch, size, zIndex)
        {
            screen = new Texture2D(spriteBatch.GraphicsDevice, size.X, size.Y);

            Color[] data = new Color[size.X * size.Y];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.Black;
            }

            screen.SetData(data);
        }

        protected override void OnDraw(int time, Rectangle destination)
        {
            spriteBatch.Draw(screen, destination, Color.Black);
        }
    }
}
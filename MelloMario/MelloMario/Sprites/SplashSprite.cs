using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Theming;

namespace MelloMario.Sprites
{
    class SplashSprite : BaseTextureSprite
    {
        private static Texture2D CreateTexture(SpriteBatch spriteBatch)
        {
            Texture2D screen = new Texture2D(spriteBatch.GraphicsDevice, GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT);

            Color[] data = new Color[GameConst.SCREEN_WIDTH * GameConst.SCREEN_HEIGHT];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.Black;
            }

            screen.SetData(data);

            return screen;
        }

        protected override void OnAnimate(int time)
        {
            // Do nothing
        }

        public SplashSprite(SpriteBatch spriteBatch) : base(
            spriteBatch,
            CreateTexture(spriteBatch),
            new Rectangle(0, 0, GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT),
            ZIndex.splash
        )
        {
        }
    }
}

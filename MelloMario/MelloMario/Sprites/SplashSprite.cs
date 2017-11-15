using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Theming;

namespace MelloMario.Sprites
{
    class SplashSprite : BaseSprite
    {
        private Texture2D screen;

        public SplashSprite(SpriteBatch spriteBatch, ZIndex zIndex = ZIndex.foreground) : base(spriteBatch, new Point(GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT), zIndex)
        {
            screen = new Texture2D(spriteBatch.GraphicsDevice, GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT);

            Color[] data = new Color[GameConst.SCREEN_WIDTH * GameConst.SCREEN_HEIGHT];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.Black;
            }

            screen.SetData(data);
        }

        protected override void OnDraw(int time, Rectangle destination)
        {
            spriteBatch.Draw(
                screen,
                destination,
                null,
                Color.Black,
                0f, //rotation
                new Vector2(), //origin
                SpriteEffects.None,
                LayerDepth
            );
        }
    }
}
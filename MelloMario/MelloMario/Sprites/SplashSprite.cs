using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Theming;

namespace MelloMario.Sprites
{
    class SplashSprite : BaseSprite
    {
        protected override void OnDraw(int time, SpriteBatch spriteBatch, Rectangle destination)
        {
            Texture2D texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.Black });

            spriteBatch.Draw(
                texture,
                destination,
                new Rectangle(0, 0, 1, 1),
                Color.Black,
                0f, //rotation
                new Vector2(), //origin
                SpriteEffects.None,
                LayerDepth
            );
        }

        public SplashSprite() : base(new Point(GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT), ZIndex.splash)
        {
        }
    }
}

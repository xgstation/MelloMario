﻿namespace MelloMario.Graphics.Sprites
{
    #region

    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class SplashSprite : BaseSprite
    {
        public SplashSprite() : base(new Point(Const.SCREEN_WIDTH, Const.SCREEN_HEIGHT), ZIndex.Splash)
        {
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch, Rectangle destination)
        {
            Texture2D texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            texture.SetData(
                new[]
                {
                    Color.Black
                });

            spriteBatch.Draw(
                texture,
                destination,
                new Rectangle(0, 0, 1, 1),
                Color.Black,
                0f, //rotation
                new Vector2(), //origin
                SpriteEffects.None,
                LayerDepth);
        }
    }
}

﻿namespace MelloMario.Graphics.Sprites
{
    #region

    using System;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class BrickPieceSprite : BaseSprite
    {
        private readonly Texture2D texture;
        private int elapsed;

        public BrickPieceSprite(Texture2D texture) : base(new Point(texture.Width, texture.Height), ZIndex.Foreground)
        {
            this.texture = texture;

            elapsed = 0;
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch, Rectangle destination)
        {
            elapsed += time;

            Vector2 origin = destination.Location.ToVector2();
            Vector2[] destinations = new Vector2[4];
            float[] offsetX = new float[4];
            float[] offsetY = new float[4];
            Rectangle[] source = new Rectangle[4];

            float k = 5f / Math.Abs(2f - Const.ACCEL_F_AIR / 20f);
            //LB
            offsetX[0] = -elapsed / k + 4f;
            offsetY[0] = Const.ACCEL_G * elapsed * elapsed / 16000f - elapsed + 16f;
            destinations[0] = new Vector2(offsetX[0], offsetY[0]) + origin;
            source[0] = new Rectangle(0, 0, texture.Width / 2, texture.Height / 2);

            //LT
            offsetX[1] = -elapsed / k + 4f;
            offsetY[1] = Const.ACCEL_G * elapsed * elapsed / 14400f - elapsed + 32f;
            destinations[1] = new Vector2(offsetX[1], offsetY[1]) + origin;
            source[1] = new Rectangle(0, texture.Height / 2, texture.Width / 2, texture.Height / 2);

            //RB
            offsetX[2] = elapsed / k + 12f;
            offsetY[2] = Const.ACCEL_G * elapsed * elapsed / 16000f - elapsed + 16f;
            destinations[2] = new Vector2(offsetX[2], offsetY[2]) + origin;
            source[2] = new Rectangle(texture.Width / 2, 0, texture.Width / 2, texture.Height / 2);

            //RT
            offsetX[3] = elapsed / k + 12f;
            offsetY[3] = Const.ACCEL_G * elapsed * elapsed / 14400f - elapsed + 32f;
            destinations[3] = new Vector2(offsetX[3], offsetY[3]) + origin;
            source[3] = new Rectangle(texture.Width / 2, texture.Height / 2, texture.Width / 2, texture.Height / 2);

            Vector2 spriteOrigin = new Vector2(8f, 8f);
            Vector2 scale = new Vector2(1.25f, 1.25f);
            float rotation = elapsed * 30f / Const.ACCEL_F_AIR;
            spriteBatch.Draw(
                texture,
                destinations[0],
                source[0],
                Color.White,
                rotation,
                spriteOrigin,
                scale,
                SpriteEffects.None,
                LayerDepth);
            spriteBatch.Draw(
                texture,
                destinations[1],
                source[1],
                Color.White,
                rotation,
                spriteOrigin,
                scale,
                SpriteEffects.None,
                LayerDepth);
            spriteBatch.Draw(
                texture,
                destinations[2],
                source[2],
                Color.White,
                -rotation,
                spriteOrigin,
                scale,
                SpriteEffects.None,
                LayerDepth);
            spriteBatch.Draw(
                texture,
                destinations[3],
                source[3],
                Color.White,
                -rotation,
                spriteOrigin,
                scale,
                SpriteEffects.None,
                LayerDepth);
        }
    }
}

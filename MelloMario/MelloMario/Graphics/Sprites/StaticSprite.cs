﻿namespace MelloMario.Graphics.Sprites
{
    #region

    using System;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class StaticSprite : BaseTextureSprite
    {
        public StaticSprite(
            Texture2D texture,
            int x = 0,
            int y = 0,
            int width = 2,
            int height = 2,
            ZIndex zIndex = ZIndex.Item) : base(
            texture,
            new Rectangle(
                x * Const.TEXTURE_GRID,
                y * Const.TEXTURE_GRID,
                width * Const.TEXTURE_GRID,
                height * Const.TEXTURE_GRID),
            zIndex)
        {
        }

        protected override void OnAnimate(int time)
        {
            // Do nothing
        }
    }
}

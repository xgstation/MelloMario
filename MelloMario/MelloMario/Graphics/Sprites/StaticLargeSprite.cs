namespace MelloMario.Graphics.Sprites
{
    #region

    using System;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    [Serializable]
    internal class StaticLargeSprite : BaseTextureSprite
    {
        public StaticLargeSprite(
            Texture2D texture,
            int x = 0,
            int y = 0,
            int width = 2,
            int height = 3,
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

namespace MelloMario
{
    #region

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal enum ZIndex
    {
        Background0,
        Background1,
        Background2,
        Background3,
        Item,
        Level,
        Foreground,
        Splash,
        Hud
    }

    internal interface ISprite
    {
        Point PixelSize { get; }

        void Draw(int time, SpriteBatch spriteBatch, Rectangle destination);
    }
}

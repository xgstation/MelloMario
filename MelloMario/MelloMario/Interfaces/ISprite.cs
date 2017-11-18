using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    enum ZIndex
    {
        background0,
        background1,
        background2,
        background3,
        item,
        level,
        foreground,
        splash,
        hud
    };

    interface ISprite
    {
        Point PixelSize { get; }

        void Draw(int time, SpriteBatch spriteBatch, Rectangle destination);
    }
}

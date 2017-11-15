using Microsoft.Xna.Framework;

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

        void Draw(int time, Rectangle destination);
    }
}

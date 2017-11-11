using Microsoft.Xna.Framework;

namespace MelloMario
{
    enum ZIndex
    {
        background0, //0.9
        background1,
        background2,
        background3,
        item,
        level,
        foreground,
        hud //0.1
    }

    interface ISprite
    {
        Point PixelSize { get; }

        void Draw(int time, Rectangle destination);
    }
}

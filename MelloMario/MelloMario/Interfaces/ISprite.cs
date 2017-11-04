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
        hud
    };

    interface ISprite
    {
        Point PixelSize { get; }

        void Draw(GameTime time, Rectangle destination, ZIndex zIndex);
    }
}

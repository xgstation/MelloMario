using Microsoft.Xna.Framework;

namespace MelloMario
{
    enum ZIndex
    {
        background,
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

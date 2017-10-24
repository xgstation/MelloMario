using Microsoft.Xna.Framework;

namespace MelloMario
{
    enum ZIndex { back, main, front };

    interface ISprite
    {
        Point PixelSize { get; }

        void Draw(GameTime time, Rectangle destination, ZIndex zIndex);
    }
}

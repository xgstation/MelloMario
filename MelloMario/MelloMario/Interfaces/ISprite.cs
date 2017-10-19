using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    enum ZIndex { back, main, front };

    interface ISprite
    {
        Point PixelSize { get; }

        void Draw(GameTime time, Rectangle destination, ZIndex zIndex);
    }
}

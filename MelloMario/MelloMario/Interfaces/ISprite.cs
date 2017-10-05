using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface ISprite
    {
        Point PixelSize { get; }

        void Draw(GameTime time, SpriteBatch spriteBatch, Rectangle destination);
    }
}

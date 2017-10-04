using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface ISprite
    {
        void Draw(SpriteBatch spriteBatch, Rectangle destination);
    }
}

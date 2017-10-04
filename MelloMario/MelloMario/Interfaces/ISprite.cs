using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface ISprite
    {
        void Draw(GameTime time, SpriteBatch spriteBatch, Rectangle destination);
    }
}

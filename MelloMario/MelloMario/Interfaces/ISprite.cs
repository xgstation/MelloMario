using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface ISprite
    {
        void Update(GameTime time);
        void Draw(SpriteBatch spriteBatch, Rectangle destination);
    }
}

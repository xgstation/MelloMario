using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface IObject
    {
        void Update(int time);
        void Draw(int time, SpriteBatch spriteBatch);
    }
}

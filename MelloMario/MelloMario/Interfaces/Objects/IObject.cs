using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    internal interface IObject
    {
        void Update(int time);
        void Draw(int time, SpriteBatch spriteBatch);
    }
}
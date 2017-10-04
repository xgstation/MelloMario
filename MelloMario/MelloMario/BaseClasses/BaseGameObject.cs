using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    abstract class BaseGameObject : IGameObject
    {
        protected Rectangle location;

        public BaseBlock(Vector2 location)
        {
            this.location = location;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

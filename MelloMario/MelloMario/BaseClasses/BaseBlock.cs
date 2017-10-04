using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects
{
    abstract class BaseBlock : BaseGameObject
    {
        protected Vector2 location;

        public BaseBlock(Vector2 location)
        {
            this.location = location;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

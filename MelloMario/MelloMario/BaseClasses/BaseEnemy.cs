using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.EnemyObjects
{
    abstract class BaseEnemy : BaseGameObject
    {
        protected Vector2 Location;

        public BaseEnemy(Vector2 initLocation)
        {
            Location = initLocation;
        }
        
        abstract public void Draw(SpriteBatch spriteBatch);

        abstract public void Update(GameTime gameTime);
    }
}

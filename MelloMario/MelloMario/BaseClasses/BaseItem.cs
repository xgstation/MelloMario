using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.ItemObjects
{
    abstract class BaseItem : BaseGameObject
    {
        public Vector2 Location { get; set; }

        public BaseItem(Vector2 initLocation)
        {
            Location = initLocation;
        }

        abstract public void Draw(SpriteBatch spriteBatch);

        abstract public void Update(GameTime gameTime);
    }
}

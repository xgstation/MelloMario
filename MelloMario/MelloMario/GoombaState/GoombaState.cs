using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprite
{
    public interface IState
    {
        void transNormal();


        void transDefeated();

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}

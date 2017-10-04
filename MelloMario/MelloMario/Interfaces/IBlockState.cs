using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface IBlockState
    {
        void ChangeToBumped();
        void ChangeToSilent();
        void ChangeToDestroyed();
        void ChangeToHidden();
        void ChangeToUsed();
        void Draw(SpriteBatch spriteBatch, Vector2 location);
        void Update(GameTime gameTime);
    }
}

using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IBlockState
    {
        void ChangeToBumped();
        void ChangeToSilent();
        void ChangeToDestroyed();
        void ChangeToHidden();
        void ChangeToUsed();

        void Update(GameTime time);
    }
}

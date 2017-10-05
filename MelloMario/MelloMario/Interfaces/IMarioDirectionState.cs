using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IMarioDirectionState
    {
        void UpgradeToRightDirection();
        void UpgradeToLeftDirection();

        void Update(GameTime time);
    }
}

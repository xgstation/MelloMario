using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IMarioMovementState
    {
        void UpgradeToMovementCrouching();
        void UpgradeToMovementldle();
        void UpgradeToMovementJumping();
        void UpgradeToMovementWalking();

        void Update(GameTime time);
    }
}

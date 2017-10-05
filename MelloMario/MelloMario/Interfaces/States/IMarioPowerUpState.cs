using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IMarioPowerUpState
    {
        void UpgradeToSuper();
        void UpgradeToFire();
        void Downgrade();
        void Kill();

        void Update(GameTime time);
    }
}

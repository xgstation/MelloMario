using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IMarioPowerUpState
    {
        void UpgradeToPowerUpDead();
        void UpgradeToPowerUpFire();
        void UpgradeToPowerUpStandard();
        void UpgradeToPowerUpSuper();

        void Update(GameTime time);
    }
}

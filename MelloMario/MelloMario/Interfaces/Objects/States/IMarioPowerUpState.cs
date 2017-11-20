namespace MelloMario
{
    internal interface IMarioPowerUpState : IState
    {
        void UpgradeToSuper();
        void UpgradeToFire();
        void Downgrade();
    }
}

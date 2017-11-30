using MelloMario.Objects.Enemies;

namespace MelloMario
{
    internal interface IMarioProtectionState : IState
    {
        void Star();
        void Protect();
        void Helmet();
    }
}

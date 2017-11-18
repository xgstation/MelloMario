using MelloMario.MarioObjects;

namespace MelloMario
{
    internal interface IBlockState : IState
    {
        void Show();
        void Hide();
        void Bump(Mario mario);
    }
}
using MelloMario.MarioObjects;

namespace MelloMario
{
    interface IBlockState : IState
    {
        void Show();
        void Hide();
        void Bump(Mario mario);
    }
}

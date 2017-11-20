namespace MelloMario
{
    #region

    using MelloMario.Objects.Characters;

    #endregion

    internal interface IBlockState : IState
    {
        void Show();
        void Hide();
        void Bump(Mario mario);
    }
}

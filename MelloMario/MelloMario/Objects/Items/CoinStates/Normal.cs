namespace MelloMario.Objects.Items.CoinStates
{
    #region

    using System;

    #endregion

    [Serializable]
    internal class Normal : BaseState<Coin>, IItemState
    {
        public Normal(Coin owner) : base(owner)
        {
        }

        public void Show()
        {
        }

        public void Collect()
        {
        }

        public override void Update(int time)
        {
        }
    }
}

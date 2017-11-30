namespace MelloMario.Objects.Items.StarStates
{
    #region

    using System;

    #endregion

    internal class Normal : BaseState<Star>, IItemState
    {
        public Normal(Star owner) : base(owner)
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

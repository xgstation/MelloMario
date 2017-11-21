namespace MelloMario.Objects.Items.OneUpMushroomStates
{
    #region

    using System;

    #endregion

    [Serializable]
    internal class Normal : BaseState<OneUpMushroom>, IItemState
    {
        public Normal(OneUpMushroom owner) : base(owner)
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

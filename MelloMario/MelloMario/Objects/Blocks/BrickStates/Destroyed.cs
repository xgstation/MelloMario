namespace MelloMario.Objects.Blocks.BrickStates
{
    #region

    using System;
    using MelloMario.Objects.Characters;

    #endregion

    [Serializable]
    internal class Destroyed : BaseTimedState<Brick>, IBlockState
    {
        public Destroyed(Brick owner) : base(owner, 1000)
        {
            //TODO: Move this into soundcontroller
            //SoundManager.BreakBlock.Play();
            Owner.OnDestroy();
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void Hide()
        {
            Owner.State = new Hidden(Owner);
        }

        public void Bump(Mario mario)
        {
            // do nothing
        }

        protected override void OnTimer(int time)
        {
            Owner.Remove();
        }
    }
}

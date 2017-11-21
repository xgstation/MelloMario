namespace MelloMario.Objects.Blocks.BrickStates
{
    #region

    using System;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Characters.PowerUpStates;

    #endregion

    [Serializable]
    internal class Normal : BaseState<Brick>, IBlockState
    {
        public Normal(Brick owner) : base(owner)
        {
        }

        public void Show()
        {
            //do nothing
        }

        public void Hide()
        {
            Owner.State = new Hidden(Owner);
        }

        public void Bump(Mario mario)
        {
            if (mario.PowerUpState is Standard || Owner.HasInitialItem)
            {
                Owner.State = new Bumped(Owner);
            }
            else
            {
                Owner.State = new Destroyed(Owner);
            }
        }

        public override void Update(int time)
        {
        }
    }
}

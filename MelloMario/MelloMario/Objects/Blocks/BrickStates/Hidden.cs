namespace MelloMario.Objects.Blocks.BrickStates
{
    #region

    using System;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Characters.PowerUpStates;
    using MelloMario.Theming;
    using MelloMario.Objects.Characters.ProtectionStates;

    #endregion

    internal class Hidden : BaseState<Brick>, IBlockState
    {
        public Hidden(Brick owner) : base(owner)
        {
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void Hide()
        {
            //do nothing
        }

        public void Bump(Mario mario)
        {
            if (Database.HasItemEnclosed(Owner))
            {
                Owner.State = new Bumped(Owner);
            }
            else
            {
                if ((mario.PowerUpState is Standard && !(mario.ProtectionState is Helmeted)) || Owner.HasInitialItem)
                {
                    Owner.State = new Bumped(Owner);
                }
                else
                {
                    Owner.State = new Destroyed(Owner);
                }
            }
        }

        public override void Update(int time)
        {
            //do nothing
        }
    }
}

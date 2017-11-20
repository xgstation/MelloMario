namespace MelloMario.Objects.Blocks.BrickStates
{
    #region

    using Characters;
    using Characters.PowerUpStates;
    using Theming;

    #endregion

    internal class Hidden : BaseState<Brick>, IBlockState
    {
        public Hidden(Brick owner) : base(owner) { }

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
                if (mario.PowerUpState is Standard || Owner.HasInitialItem)
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

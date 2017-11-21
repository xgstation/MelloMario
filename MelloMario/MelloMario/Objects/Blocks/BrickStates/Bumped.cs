namespace MelloMario.Objects.Blocks.BrickStates
{
    #region

    using MelloMario.Objects.Characters;
    using MelloMario.Theming;

    #endregion

    internal class Bumped : BaseState<Brick>, IBlockState
    {
        private int elapsed;
        private int move;

        public Bumped(Brick owner) : base(owner)
        {
            elapsed = 0;
            move = 0;
        }

        public void Show()
        {
            if (Owner.HasInitialItem && !Database.HasItemEnclosed(Owner))
            {
                Owner.State = new Used(Owner);
            }
            else
            {
                Owner.State = new Normal(Owner);
            }
        }

        public void Hide()
        {
            Owner.State = new Hidden(Owner);
        }

        public void Bump(Mario mario)
        {
            // do nothing
        }

        public override void Update(int time)
        {
            //TODO:Move this into soundcontroller
            //SoundController.BumpBlock.Play();
            // TODO: use BaseTimedState
            if (elapsed >= 100)
            {
                move = 0;
            }
            elapsed += time;
            move += 1;
            if (elapsed >= 350)
            {
                Owner.BumpMove(0);
                Owner.ReleaseNextItem();
                Show();
            }
            else if (elapsed >= 100)
            {
                Owner.BumpMove(move);
            }
            else
            {
                Owner.BumpMove(-move);
            }
        }
    }
}

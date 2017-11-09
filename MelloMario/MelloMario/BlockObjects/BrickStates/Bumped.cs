using Microsoft.Xna.Framework;
using MelloMario.MarioObjects;
using MelloMario.Theming;

namespace MelloMario.BlockObjects.BrickStates
{
    class Bumped : BaseState<Brick>, IBlockState
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
            if (Owner.HasInitialItem)
            {
                if (GameDatabase.HasItemEnclosed(Owner))
                {
                    Owner.State = new Normal(Owner);
                }
                else
                {
                    Owner.State = new Used(Owner);
                }
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

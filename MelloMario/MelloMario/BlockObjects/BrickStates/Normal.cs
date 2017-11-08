using Microsoft.Xna.Framework;
using MelloMario.MarioObjects;
using MelloMario.Theming;

namespace MelloMario.BlockObjects.BrickStates
{
    class Normal : BaseState<Brick>, IBlockState
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
            if (GameDatabase.HasItemEnclosed(Owner))
            {
                Owner.State = new Bumped(Owner);
            }
            else
            {
                if (mario.PowerUpState is MarioObjects.PowerUpStates.Standard)
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
        }
    }
}

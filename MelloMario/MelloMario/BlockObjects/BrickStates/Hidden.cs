using Microsoft.Xna.Framework;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.BrickStates
{
    class Hidden : BaseState<Brick>, IBlockState
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
            if (Owner.HasItem)
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

        public override void Update(GameTime time)
        {
            //do nothing
        }
    }
}

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
            if (Owner.Items.Count == 0 && !(mario.PowerUpState is MarioObjects.PowerUpStates.Standard))
            {
                Owner.State = new Destroyed(Owner);
            }
            else
            {
                Owner.State = new Bumped(Owner);
                Owner.ReleaseNextItem();
            }
        }

        public override void Update(GameTime time)
        {
            //do nothing
        }
    }
}

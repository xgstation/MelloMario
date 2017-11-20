using MelloMario.MarioObjects;
using MelloMario.MarioObjects.PowerUpStates;

namespace MelloMario.BlockObjects.BrickStates
{
    internal class Normal : BaseState<Brick>, IBlockState
    {
        public Normal(Brick owner) : base(owner) { }

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

        public override void Update(int time) { }
    }
}

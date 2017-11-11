using Microsoft.Xna.Framework;
using MelloMario.MarioObjects;
using MelloMario.Theming;

namespace MelloMario.BlockObjects.BrickStates
{
    class Destroyed : BaseTimedState<Brick>, IBlockState
    {
        protected override void OnTimer(int time)
        {
            Owner.Remove();
        }

        public Destroyed(Brick owner) : base(owner, 50000 / (int)GameConst.FORCE_G)
        {
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
    }
}

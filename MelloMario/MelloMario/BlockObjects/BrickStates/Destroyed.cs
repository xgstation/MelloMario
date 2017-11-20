using MelloMario.MarioObjects;
using MelloMario.Sounds;

namespace MelloMario.BlockObjects.BrickStates
{
    internal class Destroyed : BaseTimedState<Brick>, IBlockState
    {
        public Destroyed(Brick owner) : base(owner, 1000)
        {
            //TODO: Move this into soundcontroller
            //SoundController.BreakBlock.Play();
            Owner.OnDestoy();
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

        protected override void OnTimer(int time)
        {
            Owner.Remove();
        }
    }
}

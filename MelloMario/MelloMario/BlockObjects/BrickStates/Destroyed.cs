using Microsoft.Xna.Framework.Audio;
using MelloMario.MarioObjects;
using MelloMario.Sounds;

namespace MelloMario.BlockObjects.BrickStates
{
    class Destroyed : BaseTimedState<Brick>, IBlockState
    {
        protected override void OnTimer(int time)
        {
            Owner.Remove();
        }

        public Destroyed(Brick owner) : base(owner, 1000)
        {
            SoundController.BreakBlock.Play();
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
    }
}

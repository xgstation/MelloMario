using Microsoft.Xna.Framework;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.BrickStates
{
    class Destroyed : BaseState<Brick>, IBlockState
    {
        private float elapsed;

        public Destroyed(Brick owner) : base(owner)
        {
            elapsed = 0;
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

        public override void Update(GameTime time)
        {
            elapsed += time.ElapsedGameTime.Milliseconds;
            if (elapsed > 300)
            {
                Owner.Remove();
            }
        }
    }
}

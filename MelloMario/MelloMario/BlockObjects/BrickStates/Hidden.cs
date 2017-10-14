using Microsoft.Xna.Framework;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.BrickStates
{
    class Hidden : BaseState<Brick>, IBlockState
    {
        private Brick block;

        public Hidden(Brick block)
        {
            this.block = block;
        }

        public void Show()
        {
            block.State = new Normal(block);
        }

        public void Hide()
        {
            //do nothing
        }

        public void Bump(Mario mario)
        {
            //do nothing
        }

        public override void Update(GameTime time)
        {
            //do nothing
        }
    }
}

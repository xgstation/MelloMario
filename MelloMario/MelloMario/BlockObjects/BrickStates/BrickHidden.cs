using Microsoft.Xna.Framework;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.BrickStates
{
    class BrickHidden : IBlockState
    {
        private Brick block;

        public BrickHidden(Brick block)
        {
            this.block = block;
        }

        public void Show()
        {
            block.State = new BrickNormal(block);
        }

        public void Hide()
        {
            //do nothing
        }

        public void Bump(Mario mario)
        {
            //do nothing
        }

        public void Update(GameTime time)
        {
            //do nothing
        }
    }
}

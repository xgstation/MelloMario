using Microsoft.Xna.Framework;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.FloorStates
{
    class FloorHidden : IBlockState
    {
        private Floor block;

        public FloorHidden(Floor block)
        {
            this.block = block;
        }

        public void Show()
        {
            block.State = new FloorNormal(block);
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

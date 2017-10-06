using Microsoft.Xna.Framework;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.FloorStates
{
    class FloorNormal : IBlockState
    {
        private Floor block;

        public FloorNormal(Floor block)
        {
            this.block = block;
        }

        public void Show()
        {
            //do nothing
        }

        public void Hide()
        {
            block.State = new FloorHidden(block);
        }

        public void UseUp()
        {
            //stairs cant be used
        }

        public void Bump(Mario mario)
        {
            //do nothing
        }

        public void Update(GameTime time)
        {
        }
    }
}

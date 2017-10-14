using Microsoft.Xna.Framework;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.FloorStates
{
    class Normal : BaseState<Floor>, IBlockState
    {
        private Floor block;

        public Normal(Floor block)
        {
            this.block = block;
        }

        public void Show()
        {
            //do nothing
        }

        public void Hide()
        {
            //do nothing
        }

        public void UseUp()
        {
            //stairs cant be used
        }

        public void Bump(Mario mario)
        {
            //do nothing
        }

        public override void Update(GameTime time)
        {
        }
    }
}

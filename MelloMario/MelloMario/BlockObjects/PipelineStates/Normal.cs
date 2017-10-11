using Microsoft.Xna.Framework;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.PipelineStates
{
    class Normal : IBlockState
    {
        private Pipeline block;

        public Normal(Pipeline block)
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

        public void Bump(Mario mario)
        {
            //do nothing
        }

        public void Update(GameTime time)
        {
        }
    }
}

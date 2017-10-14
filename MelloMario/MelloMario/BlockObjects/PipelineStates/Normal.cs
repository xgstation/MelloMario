using Microsoft.Xna.Framework;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.PipelineStates
{
    class Normal : BaseState<Pipeline>, IBlockState
    {
        public Normal(Pipeline block)
        {
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

        public override void Update(GameTime time)
        {
        }
    }
}

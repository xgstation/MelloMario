using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Factories;

namespace MelloMario.BlockObjects.PipelineStates
{
    class PipelineNormal : IBlockState
    {
        private Pipeline block;

        public PipelineNormal(Pipeline block)
        {
            this.block = block;
        }

        public void Bump()
        {
            //do nothing
        }

        public void Destroy()
        {
            //do nothing
        }

        public void Hide()
        {
            //do nothing
        }

        public void Show()
        {
            //do nothing
        }

        public void UseUp()
        {
            //do nothing
        }

        public void Update(GameTime time)
        {
        }
    }
}

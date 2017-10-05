using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Factories;

namespace MelloMario.BlockObjects.StairStates
{
    class StairNormal : IBlockState
    {
        private Stair block;

        public StairNormal(Stair block)
        {
            this.block = block;
        }

        public void Show()
        {
            //do nothing
        }

        public void Destroy()
        {
            //cant destroy stair blocks
        }

        public void Hide()
        {
            block.State = new StairHidden(block);
        }

        public void UseUp()
        {
            //stairs cant be used
        }

        public void Bump()
        {
            //do nothing
        }

        public void Update(GameTime time)
        {
        }
    }
}

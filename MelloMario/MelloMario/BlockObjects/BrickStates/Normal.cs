using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Factories;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.BrickStates
{
    class Normal : IBlockState
    {
        private Brick block;

        public Normal(Brick block)
        {
            this.block = block;
        }

        public void Show()
        {
            //do nothing
        }

        public void Hide()
        {
            block.State = new Hidden(block);
        }

        public void Bump(Mario mario)
        {
            // TODO: if large mario && no item then
            // block.State = new BrickDestroyed(block);
            block.State = new Bumped(block);
        }

        public void Update(GameTime time)
        {
        }
    }
}

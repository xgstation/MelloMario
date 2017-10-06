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
    class BrickNormal : IBlockState
    {
        private Brick block;

        public BrickNormal(Brick block)
        {
            this.block = block;
        }

        public void Show()
        {
            //do nothing
        }

        public void Hide()
        {
            block.State = new BrickHidden(block);
        }

        public void Bump(Mario mario)
        {
            // TODO: if large mario && no item then
            // block.State = new BrickDestroyed(block);
            block.State = new BrickBumped(block);
        }

        public void Update(GameTime time)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Factories;

namespace MelloMario.BlockObjects.BrickStates
{
    class BrickDestroyed : IBlockState
    {
        private Brick block;

        public BrickDestroyed(Brick block)
        {
            this.block = block;
            // TODO: handle the case that there are 4 sprites
        }

        public void Show()
        {
            block.State = new BrickNormal(block);
        }

        public void Destroy()
        {
            //do nothing
        }

        public void Hide()
        {
            block.State = new BrickHidden(block);
        }

        public void UseUp()
        {
            block.State = new BrickUsed(block);
        }

        public void Bump()
        {
            block.State = new BrickBumped(block);
        }

        public void Update(GameTime time)
        {
        }
    }
}

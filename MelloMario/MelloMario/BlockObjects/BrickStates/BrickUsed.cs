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
    class BrickUsed : IBlockState
    {
        private Brick block;

        public BrickUsed(Brick block)
        {
            this.block = block;
        }

        public void Show()
        {
            block.State = new BrickNormal(block);
        }

        public void Destroy()
        {
            block.State = new BrickDestroyed(block);
        }

        public void Hide()
        {
            block.State = new BrickHidden(block);
        }

        public void UseUp()
        {
            //do nothing
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

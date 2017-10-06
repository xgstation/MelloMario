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

        public void Hide()
        {
            block.State = new BrickHidden(block);
        }

        public void Bump(Mario mario)
        {
            // nothing
        }

        public void Update(GameTime time)
        {
        }
    }
}

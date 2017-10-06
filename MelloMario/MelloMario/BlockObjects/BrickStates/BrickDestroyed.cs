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
    class BrickDestroyed : IBlockState
    {
        private Brick block;

        public BrickDestroyed(Brick block)
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

        public void UseUp()
        {
            // do nothing
        }

        public void Bump(Mario mario)
        {
            // do nothing
        }

        public void Update(GameTime time)
        {
        }
    }
}

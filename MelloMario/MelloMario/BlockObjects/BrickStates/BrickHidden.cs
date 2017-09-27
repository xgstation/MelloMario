using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.BrickStates
{
    class BrickHidden : IBlockState
    {
        private BrickBlock block;
        private int itemQuantity;

        public BrickHidden(BrickBlock block)
        {
            this.block = block;
        }
        public BrickHidden(BrickBlock block, int itemQuantity) : this (block)
        {
            this.itemQuantity = itemQuantity;
        }
        public void ChangeToSilent()
        {
            block.State = new BrickSilent(block);
        }

        public void ChangeToDestroyed()
        {
            block.State = new BrickDestroyed(block);
        }

        public void ChangeToHidden()
        {
            //do nothing
        }

        public void ChangeToUsed()
        {
            block.State = new BrickUsed(block);
        }

        public void ChangeToBumped()
        {
            block.State = new BrickBumped(block);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            //do nothing
        }

        public void Update(GameTime gameTime)
        {
            //do nothing
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.BrickStates
{
    class BrickSilent : IBlockState
    {
        private BrickBlock block;
        private ISprite sprite;
        private int itemQuantity;
        public BrickSilent(BrickBlock block)
        {
            this.block = block;
            sprite = SpriteFactory.Instance.CreateBrick("Silent");
            itemQuantity = 0;
        }
        public BrickSilent(BrickBlock block, int itemQuantity) : this (block)
        {
            this.itemQuantity = itemQuantity;
        }

        public void ChangeToSilent()
        {
            //do nothing
        }

        public void ChangeToDestroyed()
        {
            block.State = new BrickDestroyed(block);
        }

        public void ChangeToHidden()
        {
            block.State = new BrickHidden(block);
        }

        public void ChangeToUsed()
        {
            block.State = new BrickUsed(block);
        }

        public void ChangeToBumped()
        {
            block.State = new BrickBumped(block, itemQuantity);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }
    }
}

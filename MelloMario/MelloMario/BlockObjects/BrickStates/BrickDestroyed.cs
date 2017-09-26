using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.States
{
    class BrickDestroyed : IBlockState
    {
        private BrickBlock block;
        private ISprite sprite;

        public BrickDestroyed(BrickBlock block)
        {
            this.block = block;
            sprite = SpriteFactory.Instance.CreateBrick("Destroyed");
        }

        public void ChangeToSilent()
        {
            block.State = new BrickSilent(block);
        }

        public void ChangeToDestroy()
        {
            //do nothing
        }

        public void ChangeToHidden()
        {
            block.State = new BrickHidden(block);
        }

        public void ChangeToUsed()
        {
            block.State = new BrickUsed(block);
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

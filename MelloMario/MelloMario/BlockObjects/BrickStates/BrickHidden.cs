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
        private ISprite sprite;

        public BrickHidden(BrickBlock block)
        {
            this.block = block;
            sprite = SpriteFactory.Instance.CreateBrick("Hidden");
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
            sprite.Draw(spriteBatch, location);
        }

        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.States
{
    class BrickUsed : IBlockState
    {
        private BrickBlock block;
        private ISprite sprite;

        public BrickUsed(BrickBlock block)
        {
            this.block = block;
            sprite = SpriteFactory.Instance.CreateBrick("Used");
        }

        public void ChangeToSilent()
        {
            block.State = new BrickSilent(block);
        }

        public void ChangeToDestroy()
        {
            block.State = new BrickDestroyed(block);
        }

        public void ChangeToHidden()
        {
            block.State = new BrickHidden(block);
        }

        public void ChangeToUsed()
        {
            //do nothing
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

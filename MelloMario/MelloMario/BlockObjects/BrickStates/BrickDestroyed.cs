using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.BrickStates
{
    class BrickDestroyed : IBlockState
    {
        private BrickBlock block;
        private ISprite[] sprites;

        public BrickDestroyed(BrickBlock block)
        {
            this.block = block;
            sprites = new ISprite[4] {
                SpriteFactory.Instance.CreateBrick("DestroyedLT"),
                SpriteFactory.Instance.CreateBrick("DestroyedLB"),
                SpriteFactory.Instance.CreateBrick("DestroyedRT"),
                SpriteFactory.Instance.CreateBrick("DestroyedRB"),
            };
        }

        public void Show()
        {
            block.State = new BrickSilent(block);
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

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            foreach (ISprite sprite in sprites)
            {
                sprite.Draw(spriteBatch, location);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (ISprite sprite in sprites)
            {
                sprite.Update(gameTime);
            }
        }
    }
}

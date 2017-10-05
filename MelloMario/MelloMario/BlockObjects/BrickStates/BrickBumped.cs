using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.BrickStates
{
    class BrickBumped : IBlockState
    {
        private BrickBlock block;
        private ISprite sprite;
        private float elapsed;
        private float speedY = -7f;
        private float accelerationY = 1.5f;
        private float offset;
        public BrickBumped(BrickBlock brickBlock)
        {
            block = brickBlock;
            sprite = SpriteFactory.Instance.CreateBrick("Silent");
        }

        public void ChangeToSilent()
        {
            block.State = new BrickSilent(block);
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
            // Do nothing
        }

        public void ChangeToDestroyed()
        {
            block.State = new BrickDestroyed(block);
        }

        public void Update(GameTime time)
        {
            elapsed += ((float)gameTime.ElapsedGameTime.Milliseconds) / 20;
            offset = 0.5f * accelerationY * (float)Math.Pow(elapsed,2.0f) + speedY * elapsed;
            
            if (offset >= 0)
            {
                ChangeToSilent();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            Vector2 p = new Vector2(location.X, location.Y + offset);
            sprite.Draw(spriteBatch, p);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.FloorStates
{
    class FloorSilent : IBlockState
    {
        private FloorBlock block;
        private ISprite sprite;

        public FloorSilent(FloorBlock block)
        {
            this.block = block;
            sprite = SpriteFactory.Instance.CreateFloor("Silent");
        }

        public void ChangeToSilent()
        {
            //do nothing
        }

        public void ChangeToDestroyed()
        {
            //cant destroy stair blocks
        }

        public void ChangeToHidden()
        {
            block.State = new FloorHidden(block);
        }

        public void ChangeToUsed()
        {
            //stairs cant be used
        }

        public void ChangeToBumped()
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.FloorStates
{
    class FloorHidden : IBlockState
    {
        private FloorBlock block;
        private ISprite sprite;

        public FloorHidden(FloorBlock block)
        {
            this.block = block;
            sprite = SpriteFactory.Instance.CreateFloor("Hidden");
        }

        public void ChangeToSilent()
        {
            block.State = new FloorSilent(block);
        }

        public void ChangeToDestroyed()
        {
            //cant destroy stair blocks
        }

        public void ChangeToHidden()
        {
            //do nothing
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.StairStates
{
    class StairHidden : IBlockState
    {
        private StairBlock block;
        private ISprite sprite;

        public StairHidden(StairBlock block)
        {
            this.block = block;
            sprite = SpriteFactory.Instance.CreateStair("Hidden");
        }

        public void ChangeToSilent()
        {
            block.State = new StairSilent(block);
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

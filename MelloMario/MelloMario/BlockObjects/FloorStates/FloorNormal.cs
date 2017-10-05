using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.FloorStates
{
    class FloorNormal : IBlockState
    {
        private FloorBlock block;
        private ISprite sprite;

        public FloorNormal(FloorBlock block)
        {
            this.block = block;
            sprite = SpriteFactory.Instance.CreateFloor("Normal");
        }

        public void Show()
        {
            //do nothing
        }

        public void Destroy()
        {
            //cant destroy stair blocks
        }

        public void Hide()
        {
            block.State = new FloorHidden(block);
        }

        public void UseUp()
        {
            //stairs cant be used
        }

        public void Bump()
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

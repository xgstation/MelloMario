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
        private Brick block;

        public BrickHidden(Brick block)
        {
            this.block = block;
        }

        public void Show()
        {
            block.State = new BrickNormal(block);
        }

        public void Destroy()
        {
            block.State = new BrickDestroyed(block);
        }

        public void Hide()
        {
            //do nothing
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
            //do nothing
        }

        public void Update(GameTime gameTime)
        {
            //do nothing
        }
    }
}

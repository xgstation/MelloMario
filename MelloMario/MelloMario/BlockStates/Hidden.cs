using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockStates
{
    class Hidden : IBlockState
    {
        private IBlock block;
        public Hidden(IBlock block)
        {
            this.block = block;
        }
        public void changeToDestroy()
        {
            throw new NotImplementedException();
        }

        public void changeToHidden()
        {
            throw new NotImplementedException();
        }

        public void changeToSilent()
        {
            throw new NotImplementedException();
        }

        public void changeToUsed()
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}

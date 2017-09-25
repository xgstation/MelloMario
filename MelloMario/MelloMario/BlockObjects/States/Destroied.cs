using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.States
{
    class Destroied : IBlockState
    {
        private BaseBlock block;

        public Destroied(BaseBlock block)
        {
            this.block = block;

        }

        public void changeToSilent()
        {
            throw new NotImplementedException();
        }

        public void changeToDestroy()
        {
            
            throw new NotImplementedException();
        }

        public void changeToHidden()
        {
            throw new NotImplementedException();
        }

        public void changeToUsed()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.QuestionStates
{
    class QuestionHidden : IBlockState
    {
        private QuestionBlock block;


        public QuestionHidden(QuestionBlock block)
        {
            this.block = block;
        }

        public void ChangeToSilent()
        {
            block.State = new QuestionSilent(block);
        }

        public void ChangeToDestroyed()
        {
            //question blocks cant be destroyed
        }

        public void ChangeToHidden()
        {
            //do nothing
        }

        public void ChangeToUsed()
        {
            block.State = new QuestionUsed(block);
        }

        public void ChangeToBumped()
        {
            //do nothing
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

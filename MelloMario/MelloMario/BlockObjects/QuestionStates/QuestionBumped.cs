using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.QuestionStates
{
    class QuestionBumped : IBlockState
    {
        private Vector2 origin;
        private QuestionBlock block;
        private float elapsed;
        private float speedY = -7f;
        private float accelerationY = 1.5f;
        private float offset;
        public QuestionBumped(QuestionBlock questionBlock)
        {
            block = questionBlock;
        }

        public void ChangeToSilent()
        {
            block.State = new QuestionSilent(block);
        }

        public void ChangeToHidden()
        {
            block.State = new QuestionHidden(block);
        }

        public void ChangeToUsed()
        {
            block.State = new QuestionUsed(block);
        }

        public void ChangeToBumped()
        {
            // Do nothing
        }

        public void ChangeToDestroyed()
        {
            // Do nothing
        }

        public void Update(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            offset = 0.5f * accelerationY * (float)Math.Pow(elapsed, 2.0f) + speedY * elapsed;

            if (offset >= 0)
            {
                ChangeToSilent();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            throw new NotImplementedException();
        }
    }
}

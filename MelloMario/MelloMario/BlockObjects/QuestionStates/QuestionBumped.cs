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
        private QuestionBlock block;
        private ISprite sprite;
        private float elapsed;
        private float speedY = -7f;
        private float accelerationY = 1.5f;
        private float offset;
        private int itemQuantity;
        public QuestionBumped(QuestionBlock questionBlock)
        {
            block = questionBlock;
            sprite = SpriteFactory.Instance.CreateQuestion("Silent");
            itemQuantity = 0;
        }
        public QuestionBumped(QuestionBlock questionBlock, int itemQuantity) : this (questionBlock)
        {
            this.itemQuantity = itemQuantity;
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
            elapsed += ((float)gameTime.ElapsedGameTime.Milliseconds) / 20;
            offset = 0.5f * accelerationY * (float)Math.Pow(elapsed, 2.0f) + speedY * elapsed;

            if (offset >= 0)
            {
                ChangeToUsed();
                if (itemQuantity > 0)
                {
                    ItemObjects.FireFlower item = new ItemObjects.FireFlower(block.location);
                    block.objects.Add(item);
                }   
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            Vector2 p = new Vector2(location.X, location.Y + offset);
            sprite.Draw(spriteBatch, p);
        }
    }
}

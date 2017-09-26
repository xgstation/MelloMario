using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.States
{
    class QuestionUsed : IBlockState
    {
        private QuestionBlock block;
        private ISprite sprite;

        public QuestionUsed(QuestionBlock block)
        {
            this.block = block;
            sprite = SpriteFactory.Instance.CreateQuestion("Used");
        }

        public void ChangeToSilent()
        {
            block.State = new QuestionSilent(block);
        }

        public void ChangeToDestroy()
        {
            //question blocks cant be destroyed
        }

        public void ChangeToHidden()
        {
            block.State = new QuestionHidden(block);
        }

        public void ChangeToUsed()
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

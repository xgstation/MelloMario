using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Factories;

namespace MelloMario.BlockObjects.QuestionStates
{
    class QuestionNormal : IBlockState
    {
        private Question block;
        private ISprite sprite;

        public QuestionNormal(Question block)
        {
            this.block = block;
            sprite = SpriteFactory.Instance.CreateQuestionSprite("Normal");
        }

        public void Show()
        {
            //do nothing
        }

        public void Destroy()
        {
            //question blocks cant be destroyed
        }

        public void Hide()
        {
            block.State = new QuestionHidden(block);
        }

        public void UseUp()
        {
            block.State = new QuestionUsed(block);
        }

        public void Bump()
        {
            block.State = new QuestionBumped(block);
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

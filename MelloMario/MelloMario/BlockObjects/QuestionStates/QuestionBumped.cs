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
    class QuestionBumped : IBlockState
    {
        private Question block;
        private float elapsed;
        private float speedY = -7f;
        private float accelerationY = 1.5f;
        private float offset;
        public QuestionBumped(Question questionBlock)
        {
            block = questionBlock;
        }

        public void Show()
        {
            block.State = new QuestionNormal(block);
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
            // Do nothing
        }

        public void Destroy()
        {
            // Do nothing
        }

        public void Update(GameTime time)
        {
            // TODO: control sprite via this.block
            // elapsed += ((float)time.ElapsedGameTime.Milliseconds) / 20;
            // offset = 0.5f * accelerationY * (float)Math.Pow(elapsed, 2.0f) + speedY * elapsed;

            // if (offset >= 0)
            // {
            //     UseUp();
            // }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Factories;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.QuestionStates
{
    class Bumped : BaseState<Question>, IBlockState
    {
        private Question block;
        private float elapsed;
        private float speedY = -7f;
        private float accelerationY = 1.5f;
        private float offset;
        public Bumped(Question questionBlock)
        {
            block = questionBlock;
        }

        public void Show()
        {
            block.State = new Normal(block);
        }

        public void Hide()
        {
            block.State = new Hidden(block);
        }

        public void Bump(Mario mario)
        {
            // do nothing
        }

        public override void Update(GameTime time)
        {
            // TODO: release items
            // block.State = new QuestionNormal(block);
            // block.State = new QuestionUsed(block);

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

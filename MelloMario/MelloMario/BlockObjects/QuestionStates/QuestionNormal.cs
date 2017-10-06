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
    class QuestionNormal : IBlockState
    {
        private Question block;

        public QuestionNormal(Question block)
        {
            this.block = block;
        }

        public void Show()
        {
            //do nothing
        }

        public void Hide()
        {
            block.State = new QuestionHidden(block);
        }

        public void Bump(Mario mario)
        {
            block.State = new QuestionBumped(block);
        }

        public void Update(GameTime time)
        {
        }
    }
}

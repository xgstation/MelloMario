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
    class QuestionUsed : IBlockState
    {
        private Question block;

        public QuestionUsed(Question block)
        {
            this.block = block;
        }

        public void Show()
        {
            block.State = new QuestionNormal(block);
        }

        public void Hide()
        {
            block.State = new QuestionHidden(block);
        }

        public void Bump(Mario mario)
        {
            //do nothing
        }

        public void Update(GameTime time)
        {
        }
    }
}

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
           //do nothing
        }

        public void Bump()
        {
            //do nothing
        }

        public void Update(GameTime time)
        {
        }
    }
}

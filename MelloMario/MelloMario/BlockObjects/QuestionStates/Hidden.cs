using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.QuestionStates
{
    class Hidden : IBlockState
    {
        private Question block;


        public Hidden(Question block)
        {
            this.block = block;
        }

        public void Show()
        {
            block.State = new Normal(block);
        }

        public void Hide()
        {
            //do nothing
        }

        public void Bump(Mario mario)
        {
            //do nothing
        }

        public void Update(GameTime time)
        {
            //do nothing
        }
    }
}

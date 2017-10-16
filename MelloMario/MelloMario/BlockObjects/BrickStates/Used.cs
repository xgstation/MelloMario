using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Factories;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.BrickStates
{
    class Used : BaseState<Brick>, IBlockState
    {
        public Used(Brick block)
        {
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
            // nothing
        }

        public override void Update(GameTime time)
        {
        }
    }
}

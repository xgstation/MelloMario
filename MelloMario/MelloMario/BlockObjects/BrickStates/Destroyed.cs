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
    class Destroyed : BaseState<Brick>, IBlockState
    {
        public Destroyed(Brick block)
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

        public void UseUp()
        {
            // do nothing
        }

        public void Bump(Mario mario)
        {
            // do nothing
        }

        public override void Update(GameTime time)
        {
        }
    }
}

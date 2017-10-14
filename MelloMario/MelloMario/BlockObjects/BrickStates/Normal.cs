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
    class Normal : BaseState<Brick>, IBlockState
    {
        public Normal(Brick block)
        {
        }

        public void Show()
        {
            //do nothing
        }

        public void Hide()
        {
            block.State = new Hidden(block);
        }

        public void Bump(Mario mario)
        {
            // TODO: if large mario && no item then
            // block.State = new BrickDestroyed(block);
            block.State = new Bumped(block);
        }

        public override void Update(GameTime time)
        {
        }
    }
}

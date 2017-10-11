using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Factories;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.StairStates
{
    class Normal : IBlockState
    {
        private Stair block;

        public Normal(Stair block)
        {
            this.block = block;
        }

        public void Show()
        {
            //do nothing
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
        }
    }
}

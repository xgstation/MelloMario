using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario.MarioObjects.DirectionStates
{
    class Left : BaseState<Mario>, IMarioDirectionState
    {
        public Left(Mario owner) : base(owner)
        {
        }

        public void TurnLeft()
        {

        }

        public void TurnRight()
        {
            Owner.DirectionState = new Right(Owner);
        }

        public override void Update(GameTime time)
        {
        }

    }
}

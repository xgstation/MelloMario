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
    class Right : BaseState<Mario>, IMarioDirectionState
    {
        public Right(Mario owner) : base(owner)
        {
        }

        public void TurnLeft()
        {
            Owner.DirectionState = new Left(Owner);
        }

        public void TurnRight()
        {

        }

        public override void Update(GameTime time)
        {
        }
    }
}

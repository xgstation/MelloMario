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
        Mario mario;

        public Left(Mario newMario)
        {
            mario = newMario;
        }

        public void TurnLeft()
        {

        }

        public void TurnRight()
        {
            mario.DirectionState = new Right(mario);
        }

        public override void Update(GameTime time)
        {
        }

    }
}

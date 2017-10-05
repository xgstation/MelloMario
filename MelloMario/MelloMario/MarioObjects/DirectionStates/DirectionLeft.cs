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
    class DirectionLeft : IMarioDirectionState
    {
        Mario mario;

        public DirectionLeft(Mario newMario)
        {
            mario = newMario;
        }

        public void TurnLeft()
        {

        }

        public void TurnRight()
        {
            mario.DirectionState = new DirectionRight(mario);
        }

        public void Update(GameTime time)
        {
        }

    }
}

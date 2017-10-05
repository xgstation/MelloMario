﻿using System;
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
    class DirectionRight : IMarioDirectionState
    {
        Mario mario;

        public DirectionRight(Mario newMario)
        {
            mario = newMario;
        }

        public void TurnLeft()
        {
            mario.DirectionState = new DirectionLeft(mario);
        }

        public void TurnRight()
        {

        }

        public void Update(GameTime game)
        {
        }
    }
}
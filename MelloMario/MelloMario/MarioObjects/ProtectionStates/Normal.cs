﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario.MarioObjects.ProtectionStates
{
    class Normal : BaseState<Mario>, IMarioProtectionState
    {
        public Normal(Mario owner) : base(owner)
        {
        }

        public void Star()
        {
            Owner.ProtectionState = new Starred(Owner);
        }

        public override void Update(GameTime time)
        {

        }
    }
}

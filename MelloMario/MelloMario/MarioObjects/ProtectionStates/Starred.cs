using System;
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
    class Starred : BaseState<Mario>, IMarioProtectionState
    {
        Mario mario;

        public Starred(Mario mario)
        {
            this.mario = mario;
        }

        public void Star()
        {

        }

        public override void Update(GameTime time)
        {

        }
    }
}

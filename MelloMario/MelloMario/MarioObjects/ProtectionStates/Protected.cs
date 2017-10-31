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
    class Protected : BaseTimedState<Mario>, IMarioProtectionState
    {
        protected override void OnTimer(GameTime time)
        {
            Owner.ProtectionState = new Normal(Owner);
        }

        public Protected(Mario owner) : base(owner, 1000)
        {
        }

        public void Star()
        {
            Owner.ProtectionState = new Starred(Owner);
        }
    }
}

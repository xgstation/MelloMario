using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.MarioObjects.PowerUpStates;

namespace MelloMario.MarioObjects.MovementStates
{
    class Standing : BaseState<Mario>, IMarioMovementState
    {
        public Standing(Mario owner) : base(owner)
        {
        }

        public void Crouch()
        {
            Owner.MovementState = new Crouching(Owner);
        }
        public void Idle()
        {

        }

        public void Land()
        {

        }
        public void Jump()
        {
            Owner.MovementState = new Jumping(Owner);
        }
        public void Walk()
        {
            Owner.MovementState = new Walking(Owner);
        }

        public override void Update(GameTime time)
        {
            //sprite.Update(game);
        }
    }
}

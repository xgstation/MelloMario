using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.MarioObjects.MovementStates
{
    class Crouching : BaseState<Mario>, IMarioMovementState
    {
        public Crouching(Mario owner) : base(owner)
        {
        }

        public void Crouch()
        {

        }
        public void Idle()
        {
            Owner.MovementState = new Standing(Owner);
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

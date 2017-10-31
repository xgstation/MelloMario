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
    class Jumping : BaseTimedState<Mario>, IMarioMovementState
    {
        private IMarioMovementState previous;
        private bool finished;

        protected override void OnTimer(GameTime time)
        {
            finished = true;
        }

        public bool Finished
        {
            get
            {
                return finished;
            }
            set
            {
                finished = value; // TODO: use a better solution for "free jump/fall"
            }
        }

        public Jumping(Mario owner) : base(owner, 200)
        {
            previous = owner.MovementState;
            finished = false;
        }

        public void Crouch()
        {
            Owner.MovementState = new Crouching(Owner);
        }
        public void Idle()
        {
            previous = new Standing(Owner);
        }
        public void Land()
        {
            Owner.MovementState = previous;
        }
        public void Jump()
        {

        }
        public void Walk()
        {
            previous = new Walking(Owner);
        }
    }
}

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
    class MovementCrouching : IMarioMovementState
    {
        Mario mario;

        public MovementCrouching(Mario mario)
        {
            this.mario = mario;
        }
        public void Crouch()
        {

        }
        public void Idle()
        {
            this.mario.MovementState = new Movementldle(this.mario);
        }
        public void Jump()
        {
            this.mario.MovementState = new MovementJumping(this.mario);
        }
        public void Walk()
        {
            this.mario.MovementState = new MovementWalking(this.mario);
        }

        public void Update(GameTime time)
        {
            //sprite.Update(game);
        }
    }
}

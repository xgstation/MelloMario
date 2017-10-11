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
    class Crouching : IMarioMovementState
    {
        Mario mario;

        public Crouching(Mario mario)
        {
            this.mario = mario;
        }
        public void Crouch()
        {

        }
        public void Idle()
        {
            this.mario.MovementState = new Standing(this.mario);
        }
        public void Jump()
        {
            this.mario.MovementState = new Jumping(this.mario);
        }
        public void Walk()
        {
            this.mario.MovementState = new Walking(this.mario);
        }

        public void Update(GameTime time)
        {
            //sprite.Update(game);
        }
    }
}

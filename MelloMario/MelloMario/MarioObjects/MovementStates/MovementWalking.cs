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
    class MovementWalking : IMarioMovementState
    {
        Mario mario;
        //bool setToStatic;
        // ISprite sprite;
        public MovementWalking(Mario mario)
        {
            this.mario = mario;
            //setToStatic = true;
            //sprite = SpriteFactory.Instance.CreateMarioSprite("FireCrouchingLeft", setToStatic);

        }
        public void Crouch()
        {
            this.mario.MovementState = new MovementCrouching(this.mario);
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

        }

        public void Update(GameTime time)
        {
            //sprite.Update(game);
        }
    }
}

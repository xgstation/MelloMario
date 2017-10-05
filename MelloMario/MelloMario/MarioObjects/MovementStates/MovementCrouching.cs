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
        bool setToStatic;
        ISprite sprite;
        public MovementCrouching(Mario mario)
        {
            this.mario = mario;
            //setToStatic = true;
            //sprite = SpriteFactory.Instance.CreateMarioSprite("FireCrouchingLeft", setToStatic);
            
        }
        public void Crouch()
        {
          
        }
        public void Idle()
        {
            this.mario.State = new Movementldle(this.mario);
        }
        public void Jump()
        {
            this.mario.State = new MovementJumping(this.mario);
        }
        public void Walk()
        {
            this.mario.State = new MovementWalking(this.mario);
        }

        public void Update(GameTime time)
        {
            //sprite.Update(game);
        }
    }
}

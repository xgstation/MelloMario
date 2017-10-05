using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario.MarioObjects.MovementStates
{
    class Movementldle : IMarioMovementState
    {
        Mario mario;
        //bool setToStatic;
       // ISprite sprite;
        public Movementldle(Mario mario)
        {
            this.mario = mario;
            //setToStatic = true;
            //sprite = SpriteFactory.Instance.CreateMarioSprite("FireCrouchingLeft", setToStatic);

        }
        public void Crouch()
        {
            this.mario.State = new MovementCrouching(this.mario);
        }
        public void Idle()
        {
            
        }
        public void Jump()
        {
            this.mario.State = new MovementJumping(this.mario);
        }
        public void Walk()
        {
            this.mario.State = new MovementWalking(this.mario);
        }

        public void Update(GameTime game)
        {
            //sprite.Update(game);
        }
    }
}

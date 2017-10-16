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
    class Standing : BaseState<Mario>, IMarioMovementState
    {
        Mario mario;
        //bool setToStatic;
        // ISprite sprite;
        public Standing(Mario mario)
        {
            this.mario = mario;
            //setToStatic = true;
            //sprite = SpriteFactory.Instance.CreateMarioSprite("FireCrouchingLeft", setToStatic);

        }
        public void Crouch()
        {
            this.mario.MovementState = new Crouching(this.mario);
        }
        public void Idle()
        {

        }
        public void Jump()
        {
            this.mario.MovementState = new Jumping(this.mario);
        }
        public void Walk()
        {
            this.mario.MovementState = new Walking(this.mario);
        }

        public override void Update(GameTime time)
        {
            //sprite.Update(game);
        }
    }
}

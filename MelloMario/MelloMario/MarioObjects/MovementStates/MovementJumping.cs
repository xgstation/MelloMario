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
    class MovementJumping : IMarioMovementState
    {
        Mario mario;
        //bool setToStatic;
        // ISprite sprite;
        public MovementJumping(Mario mario)
        {
            this.mario = mario;
            //setToStatic = true;
            //sprite = SpriteFactory.Instance.CreateMarioSprite("FireCrouchingLeft", setToStatic);

        }
        public void UpgradeToMovementCrouching()
        {
            this.mario.State = new MovementCrouching(this.mario);
        }
        public void UpgradeToMovementldle()
        {
            this.mario.State = new Movementldle(this.mario);
        }
        public void UpgradeToMovementJumping()
        {
            
        }
        public void UpgradeToMovementWalking()
        {
            this.mario.State = new MovementWalking(this.mario);
        }

        public void Update(GameTime game)
        {
            //sprite.Update(game);
        }
    }
}

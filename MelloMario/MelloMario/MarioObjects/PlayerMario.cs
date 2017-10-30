using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.MarioObjects.PowerUpStates;
using MelloMario.MarioObjects.ProtectionStates;
using MelloMario.BlockObjects;
using MelloMario.EnemyObjects;
using System;

namespace MelloMario.MarioObjects
{
    class PlayerMario : Mario, IGameCharacter
    {
        private Vector2 userInput;

        protected override void OnUpdate(GameTime time)
        {
            // TODO
            elapsedFromPreviousFrame += time.ElapsedGameTime.Milliseconds;

            ApplyForce(userInput);
            userInput.X = 0;
            userInput.Y = 0;
            base.OnUpdate(time);
        }

        public Rectangle Viewport
        {
            get
            {
                Point location = Boundary.Location - new Point(320, 320);
                Point size = new Point(800, 480); // TODO: currently, size of viewport does nothing
                return new Rectangle(location, size);
            }
        }

        public PlayerMario(IGameWorld world, Point location) : base(world, location)
        {
        }

        public void Left()
        {
            if (!(MovementState is Crouching))
            {
                userInput.X -= FORCE_INPUT;
            }
            if (MovementState is Standing)
            {
                MovementState.Walk();
            }
        }

        public void LeftPress()
        {
            if (Facing == FacingMode.right)
            {
                ChangeFacing(FacingMode.left);
            }
        }

        public void LeftRelease()
        {
            if (MovementState is Walking)
            {
                MovementState.Idle();
            }
        }

        public void Right()
        {
            if (!(MovementState is Crouching))
            {
                userInput.X += FORCE_INPUT;
            }
            if (MovementState is Standing)
            {
                MovementState.Walk();
            }
        }

        public void RightPress()
        {
            if (Facing == FacingMode.left)
            {
                ChangeFacing(FacingMode.right);
            }
        }

        public void RightRelease()
        {
            if (MovementState is Walking)
            {
                MovementState.Idle();
            }
        }

        int secondsPerFrame = 500; //Update every half second
        int elapsedFromPreviousFrame = 0; //Accumulate the elapsed time
        public void Jump()
        {
            if (!(MovementState is Jumping) || elapsedFromPreviousFrame < secondsPerFrame)
            {
                userInput.Y -= FORCE_INPUT;
                userInput.Y -= FORCE_G;
                
                MovementState.Jump();
            }
        }

        public void JumpPress()
        {
            if (MovementState is Standing)
                elapsedFromPreviousFrame = 0;
        }

        public void JumpRelease()
        {
            //if ((MovementState is Jumping))
            //{
            //    isFalling = true;
            //} 
            //else
            //{
            //    isFalling = false;
            //}
            // if (MovementState is Walking)
            // {
            //if (Facing == FacingMode.left)
            // {
            //  Left();
            // LeftRelease();
            //  }
            // else
            //{
            // Right();
            // RightRelease();
            //}
            // }
        }

        public void Crouch()
        {
            MovementState.Crouch();
        }

        public void CrouchPress()
        {
        }

        public void CrouchRelease()
        {
            MovementState.Idle();
        }

        public void Action()
        {
            throw new System.NotImplementedException();
        }
    }
}

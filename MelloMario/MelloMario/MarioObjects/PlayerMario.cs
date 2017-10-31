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
                userInput.X -= FORCE_INPUT_X;
            }
        }

        public void LeftPress()
        {
            if (Facing == FacingMode.right)
            {
                ChangeFacing(FacingMode.left);
            }
            MovementState.Walk();

            Left();
        }

        public void LeftRelease()
        {
            MovementState.Idle();
        }

        public void Right()
        {
            if (!(MovementState is Crouching))
            {
                userInput.X += FORCE_INPUT_X;
            }
        }

        public void RightPress()
        {
            if (Facing == FacingMode.left)
            {
                ChangeFacing(FacingMode.right);
            }
            MovementState.Walk();

            Right();
        }

        public void RightRelease()
        {
            MovementState.Idle();
        }

        public void Jump()
        {
            if (MovementState is Jumping jumping && !jumping.Finished)
            {
                userInput.Y -= FORCE_INPUT_Y;
                userInput.Y -= FORCE_G;
            }
        }

        public void JumpPress()
        {
            MovementState.Jump();

            Jump();
        }

        public void JumpRelease()
        {
            if (MovementState is Jumping jumping)
            {
                jumping.Finished = true;
            }
        }

        public void Crouch()
        {
        }

        public void CrouchPress()
        {
            MovementState.Crouch();

            Crouch();
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

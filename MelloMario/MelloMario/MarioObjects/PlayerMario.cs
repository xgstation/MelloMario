using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.MarioObjects.PowerUpStates;
using MelloMario.MarioObjects.ProtectionStates;
using MelloMario.BlockObjects;
using MelloMario.EnemyObjects;

namespace MelloMario.MarioObjects
{
    class PlayerMario : Mario, IGameCharacter
    {
        private Vector2 userInput;

        protected override void OnSimulation(GameTime time)
        {
            ApplyForce(userInput);
            userInput.X = 0;
            userInput.Y = 0;

            base.OnSimulation(time);
        }

        public Rectangle Viewport
        {
            get
            {
                // TODO
                return new Rectangle(0, 0, 0, 0);
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

        public void Jump()
        {
            if (!(MovementState is Crouching))
            {
                userInput.Y -= FORCE_INPUT;
                userInput.Y -= FORCE_G;

                MovementState.Jump();
            }
        }

        public void JumpPress()
        {
            // TODO: for sprint2 only
            Bounce(CollisionMode.Bottom, new Vector2());
        }

        public void JumpRelease()
        {
            // TODO: for sprint2 only
            if (MovementState is Crouching)
            {
                MovementState.Idle();
            }
        }

        public void Crouch()
        {
            if (!(MovementState is Jumping))
            {
                // TODO: for sprint2 only
                // "fall" instead of "crouch"
                userInput.Y += FORCE_INPUT;

                MovementState.Crouch();
            }
        }

        public void CrouchPress()
        {
            // TODO: for sprint2 only
            Bounce(CollisionMode.Top, new Vector2());
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

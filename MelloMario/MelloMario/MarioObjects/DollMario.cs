using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.Theming;
using MelloMario.Sounds;

namespace MelloMario.MarioObjects
{
    class DollMario : Mario, ICharacter
    {
        private bool active;
        private Vector2 userInput;
        private SoundEffectInstance jumpSound;
        private SoundEffectInstance powerJumpSound;

        protected override void OnUpdate(int time)
        {
            if (active)
            {
                ApplyForce(userInput);
                userInput.X = 0;
                userInput.Y = 0;
            }

            base.OnUpdate(time);
        }

        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }

        public Rectangle Sensing
        {
            get
            {
                Point location = Boundary.Location - new Point(GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT);
                Point size = new Point(GameConst.SCREEN_WIDTH * 2, GameConst.SCREEN_HEIGHT * 2); // notice: should be greater than viewport

                return new Rectangle(location, size);
            }
        }

        public Rectangle Viewport
        {
            get
            {
                Point location = Boundary.Location - new Point(GameConst.FOCUS_X, GameConst.FOCUS_Y);
                Point size = new Point(GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT);

                Rectangle worldBoundary = World.Boundary;

                // NOTE: this is a temporary solution, this should be moved to the collision detection system
                if (location.X < worldBoundary.Left)
                {
                    location.X = worldBoundary.Left;
                }
                if (location.Y < worldBoundary.Top)
                {
                    location.Y = worldBoundary.Top;
                }
                if (location.X > worldBoundary.Right - size.X)
                {
                    location.X = worldBoundary.Right - size.X;
                }
                if (location.Y > worldBoundary.Bottom - size.Y)
                {
                    location.Y = worldBoundary.Bottom - size.Y;
                }

                return new Rectangle(location, size);
            }
        }

        public DollMario(IGameWorld world, Point location, Listener listener) : base(world, location, listener)
        {
            active = true;
            jumpSound = SoundController.bounce.CreateInstance();
            powerJumpSound = SoundController.powerBounce.CreateInstance();
        }

        public void Left()
        {
            if (active)
            {
                MovementState.Walk();

                if (Facing == FacingMode.right)
                {
                    ChangeFacing(FacingMode.left);
                }

                if (!(MovementState is Crouching))
                {
                    userInput.X -= GameConst.FORCE_INPUT_X;
                }
            }
        }

        public void LeftPress()
        {
            if (active)
            {
                Left();
            }
        }

        public void LeftRelease()
        {
            if (active)
            {
                MovementState.Idle();
            }
        }

        public void Right()
        {
            if (active)
            {
                MovementState.Walk();

                if (Facing == FacingMode.left)
                {
                    ChangeFacing(FacingMode.right);
                }

                if (!(MovementState is Crouching))
                {
                    userInput.X += GameConst.FORCE_INPUT_X;
                }
            }
        }

        public void RightPress()
        {
            if (active)
            {
                Right();
            }
        }

        public void RightRelease()
        {
            if (active)
            {
                MovementState.Idle();
            }
        }

        public void Jump()
        {
            if (active)
            {
                MovementState.Jump();

                if (MovementState is Jumping jumping && !jumping.Finished)
                {
                    userInput.Y -= GameConst.FORCE_INPUT_Y;
                    userInput.Y -= GameConst.FORCE_G;
                }
            }
        }

        public void JumpPress()
        {
            if (active)
            {
                if (PowerUpState is PowerUpStates.Super || PowerUpState is PowerUpStates.Fire)
                {
                    powerJumpSound.Play();
                }
                else
                {
                    jumpSound.Play();
                }

                Jump();
            }
        }

        public void JumpRelease()
        {
            if (active)
            {
                if (MovementState is Jumping jumping)
                {
                    jumping.Finished = true;
                }
            }
        }

        public void Crouch()
        {
            if (active)
            {
                MovementState.Crouch();
            }
        }

        public void CrouchPress()
        {
            if (active)
            {
                Crouch();
            }
        }

        public void CrouchRelease()
        {
            if (active)
            {
                MovementState.Idle();
            }
        }

        public void Action()
        {
            if (active)
            {
                if (PowerUpState is PowerUpStates.Fire)
                {
                    // note: listener is passed as null so score points will not do anything
                    new Fire(World, Boundary.Location, null, Facing == FacingMode.right);
                }
            }
        }
    }
}

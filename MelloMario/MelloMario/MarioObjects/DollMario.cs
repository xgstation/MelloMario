using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.Theming;
using MelloMario.Sounds;

namespace MelloMario.MarioObjects
{
    class DollMario : Mario, ICharacter
    {
        // TODO: create AnimationStates
        private enum Animation
        {
            none,
            teleport,
            flagpole
        }

        private Animation animation;
        private int toBeTraveled;

        private Vector2 userInput;
        private SoundEffectInstance jumpSound;
        private SoundEffectInstance powerJumpSound;

        protected void Teleport()
        {
            animation = Animation.teleport;
            toBeTraveled = Boundary.Height;
            // TODO: initialize
        }

        protected void FlagPole()
        {
            animation = Animation.flagpole;
        }

        protected override void OnSimulation(int time)
        {
            if (animation == Animation.teleport)
            {
                Move(new Point(0, 2));

                toBeTraveled -= 2;
                if (toBeTraveled <= 0)
                {
                    animation = Animation.none;
                }
            }
            base.OnSimulation(time);
        }

        protected override void OnUpdate(int time)
        {
            if (animation == Animation.none)
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
                return animation == Animation.none;
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
            jumpSound = SoundController.bounce.CreateInstance();
            powerJumpSound = SoundController.powerBounce.CreateInstance();
        }

        public void Left()
        {
            if (animation == Animation.none)
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
            if (animation == Animation.none)
            {
                Left();
            }
        }

        public void LeftRelease()
        {
            if (animation == Animation.none)
            {
                MovementState.Idle();
            }
        }

        public void Right()
        {
            if (animation == Animation.none)
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
            if (animation == Animation.none)
            {
                Right();
            }
        }

        public void RightRelease()
        {
            if (animation == Animation.none)
            {
                MovementState.Idle();
            }
        }

        public void Jump()
        {
            if (animation == Animation.none)
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
            if (animation == Animation.none)
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
            if (animation == Animation.none)
            {
                if (MovementState is Jumping jumping)
                {
                    jumping.Finished = true;
                }
            }
        }

        public void Crouch()
        {
            if (animation == Animation.none)
            {
                MovementState.Crouch();
            }
        }

        public void CrouchPress()
        {
            if (animation == Animation.none)
            {
                Crouch();
            }
        }

        public void CrouchRelease()
        {
            if (animation == Animation.none)
            {
                MovementState.Idle();
            }
        }

        public void Action()
        {
            if (animation == Animation.none)
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

using MelloMario.Containers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.Theming;
using MelloMario.Sounds;

namespace MelloMario.MarioObjects
{
    class PlayerMario : Mario, IPlayer
    {
        private Vector2 userInput;
        private SoundEffectInstance jumpSound;

        protected IGameSession Session;

        protected override void OnUpdate(int time)
        {
            ApplyForce(userInput);
            userInput.X = 0;
            userInput.Y = 0;
            base.OnUpdate(time);
        }

        public IGameWorld CurrentWorld
        {
            get
            {
                return World;
            }
        }

        public Rectangle Sensing
        {
            get
            {
                Point location = Boundary.Location - new Point(800, 600);
                Point size = new Point(1600, 1200); // notice: should be greater than viewport

                return new Rectangle(location, size);
            }
        }

        public Rectangle Viewport
        {
            get
            {
                Point location = Boundary.Location - new Point(320, 320);
                Point size = new Point(800, 600); // TODO: should be the same as resolution

                Rectangle worldBoundary = World.Boundary;

                // NOTE: this is a temporary solution for sprint 3, this should be moved to the collision detection system
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

        public PlayerMario(IGameSession session, IGameWorld world, Point location) : base(world, location)
        {
            Session = session;
            Session.Add(this);

            Relocate(World.GetInitialPoint());

            jumpSound = SoundController.bounce.CreateInstance();
        }

        public void Left()
        {
            MovementState.Walk();

            if (!(MovementState is Crouching))
            {
                userInput.X -= GameConst.FORCE_INPUT_X;
            }
        }

        public void LeftPress()
        {
            if (Facing == FacingMode.right)
            {
                ChangeFacing(FacingMode.left);
            }

            Left();
        }

        public void LeftRelease()
        {
            MovementState.Idle();
        }

        public void Right()
        {
            MovementState.Walk();

            if (!(MovementState is Crouching))
            {
                userInput.X += GameConst.FORCE_INPUT_X;
            }
        }

        public void RightPress()
        {
            if (Facing == FacingMode.left)
            {
                ChangeFacing(FacingMode.right);
            }

            Right();
        }

        public void RightRelease()
        {
            MovementState.Idle();
        }

        public void Jump()
        {
            MovementState.Jump();

            if (MovementState is Jumping jumping && !jumping.Finished)
            {
                userInput.Y -= GameConst.FORCE_INPUT_Y;
                userInput.Y -= GameConst.FORCE_G;
            }
        }

        public void JumpPress()
        {
            jumpSound.Play();
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
            MovementState.Crouch();
        }

        public void CrouchPress()
        {
            Crouch();
        }

        public void CrouchRelease()
        {
            MovementState.Idle();
        }

        public void Action()
        {
        }

        public void Spawn(IGameWorld world)
        {
            World.Remove(this);
            World = world;
            World.Add(this);

            Session.Move(this);

            Relocate(World.GetInitialPoint());
        }

        public void Reset()
        {
            RemoveSelf();
            Session.Remove(this);

            // note: Boundary.Location or Boundary.Center? sometimes confusing
            new PlayerMario(Session, World, World.GetRespawnPoint(Boundary.Location));
        }
    }
}

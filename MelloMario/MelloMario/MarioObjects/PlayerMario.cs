using Microsoft.Xna.Framework;
using MelloMario.MarioObjects.MovementStates;

namespace MelloMario.MarioObjects
{
    class PlayerMario : Mario, IPlayer
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
                Point size = new Point(800, 600); // TODO: currently, size of viewport does nothing

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

        public PlayerMario(IGameWorld world, Point location) : base(world, location)
        {
        }

        public void Left()
        {
            MovementState.Walk();

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
                userInput.X += FORCE_INPUT_X;
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
                userInput.Y -= FORCE_INPUT_Y;
                userInput.Y -= FORCE_G;
            }
        }

        public void JumpPress()
        {
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
            throw new System.NotImplementedException();
        }

        public void Spawn(IGameWorld world)
        {
            World.Remove(this);
            World = world;
            Relocate(world.GetRespawnPoint(new Point(Boundary.Center.X, Boundary.Bottom)));
        }

    }
}

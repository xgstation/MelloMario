using Microsoft.Xna.Framework;

namespace MelloMario
{
    abstract class BasePhysicalObject : BaseCollidableObject
    {
        private const float VELOCITY_MAX_LR = 10f;
        private const float VELOCITY_MAX_U = 15f;
        private const float VELOCITY_MAX_D = 20f;

        private float pixelScale;
        private Vector2 movement;
        private Vector2 velocity;
        private Vector2 force;
        private Vector2 frictionalForce;

        // TODO: make this private again once we have a better collision event dispatch mechanism
        //       a goomba/koopa should "know" in which case it can hurt/bounce mario
        //       instead of doing runtime type-checking on all enemys in mario's class
        public enum FacingMode { left, right };

        protected const float FORCE_G = 40f;
        protected const float FORCE_INPUT_X = 120f;
        protected const float FORCE_INPUT_Y = 150f;
        protected const float FORCE_F_GROUND = 100f;
        protected const float FORCE_F_AIR = 20f;

        public FacingMode Facing;

        protected void ApplyForce(Vector2 delta)
        {
            force += delta;
        }

        protected void ApplyGravity(float gravity = FORCE_G)
        {
            ApplyForce(new Vector2(0, gravity));
        }

        protected void ApplyHorizontalFriction(float friction)
        {
            if (frictionalForce.X < friction)
            {
                frictionalForce.X = friction;
            }
        }

        protected void ApplyVerticalFriction(float friction)
        {
            if (frictionalForce.Y < friction)
            {
                frictionalForce.Y = friction;
            }
        }

        protected bool Bounce(CollisionMode mode, Vector2 refVelocity, float rate = 0)
        {
            switch (mode)
            {
                case CollisionMode.Left:
                case CollisionMode.InnerLeft:
                    if (velocity.X < refVelocity.X)
                    {
                        // movement.X = 0;
                        velocity.X = refVelocity.X + (refVelocity.X - velocity.X) * rate;
                        StopHorizontalMovement();

                        return true;
                    }

                    return false;
                case CollisionMode.Right:
                case CollisionMode.InnerRight:
                    if (velocity.X > refVelocity.X)
                    {
                        // movement.X = 0;
                        velocity.X = refVelocity.X + (refVelocity.X - velocity.X) * rate;
                        StopHorizontalMovement();

                        return true;
                    }

                    return false;
                case CollisionMode.Top:
                case CollisionMode.InnerTop:
                    if (velocity.Y < refVelocity.Y)
                    {
                        // movement.Y = 0;
                        velocity.Y = refVelocity.Y + (refVelocity.Y - velocity.Y) * rate;
                        StopVerticalMovement();

                        return true;
                    }

                    return false;
                case CollisionMode.Bottom:
                case CollisionMode.InnerBottom:
                    if (velocity.Y > refVelocity.Y)
                    {
                        // movement.Y = 0;
                        velocity.Y = refVelocity.Y + (refVelocity.Y - velocity.Y) * rate;
                        StopVerticalMovement();

                        return true;
                    }

                    return false;
                default:
                    // never reach
                    return false;
            }
        }

        protected override void OnSimulation(GameTime time)
        {
            // Note: if we will support recording/replaying, use a constant number here
            float deltaTime = time.ElapsedGameTime.Milliseconds / 1000f;

            // Apply conservative force

            velocity += force * deltaTime;
            force.X = 0;
            force.Y = 0;

            // Apply frictional force

            Vector2 deltaV = frictionalForce * deltaTime;

            if (velocity.X > deltaV.X)
            {
                velocity.X -= deltaV.X;
            }
            else if (velocity.X < -deltaV.X)
            {
                velocity.X += deltaV.X;
            }
            else
            {
                velocity.X = 0;
            }

            if (velocity.Y > deltaV.Y)
            {
                velocity.Y -= deltaV.Y;
            }
            else if (velocity.Y < -deltaV.Y)
            {
                velocity.Y += deltaV.Y;
            }
            else
            {
                velocity.Y = 0;
            }

            frictionalForce.X = 0;
            frictionalForce.X = 0;

            // Apply velocity

            if (velocity.X > VELOCITY_MAX_LR)
            {
                velocity.X = VELOCITY_MAX_LR;
            }
            else if (velocity.X < -VELOCITY_MAX_LR)
            {
                velocity.X = -VELOCITY_MAX_LR;
            }
            if (velocity.Y > VELOCITY_MAX_D)
            {
                velocity.Y = VELOCITY_MAX_D;
            }
            else if (velocity.Y < -VELOCITY_MAX_U)
            {
                velocity.Y = -VELOCITY_MAX_U;
            }
            movement += velocity * deltaTime;

            // Apply movement

            Point pixelMovement = (movement * pixelScale).ToPoint();
            movement -= pixelMovement.ToVector2() / pixelScale;
            Move(pixelMovement);

            base.OnSimulation(time);
        }

        public BasePhysicalObject(IGameWorld world, Point location, Point size, float pixelScale) : base(world, location, size)
        {
            this.pixelScale = pixelScale;
            movement = new Vector2();
            velocity = new Vector2();
            force = new Vector2();
            Facing = FacingMode.right;
        }
    }
}

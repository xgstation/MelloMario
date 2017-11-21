namespace MelloMario.Objects
{
    #region

    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal abstract class BasePhysicalObject : BaseCollidableObject
    {
        private readonly float pixelScale;
        protected FacingMode Facing;
        private Vector2 accel;
        private Vector2 frictionalAccel;
        private Vector2 velocity;
        private Vector2 movement;

        protected BasePhysicalObject(IGameWorld world, Point location, IListener<IGameObject> listener, Point size, float pixelScale) : base(world, location, listener, size)
        {
            this.pixelScale = pixelScale;

            Facing = FacingMode.right;
        }

        protected void ApplyAccel(Vector2 delta)
        {
            accel += delta;
        }

        protected void ApplyGravity(float gravity = Const.ACCEL_G)
        {
            ApplyAccel(new Vector2(0, gravity));
        }

        protected void ApplyHorizontalFriction(float friction)
        {
            if (frictionalAccel.X < friction)
            {
                frictionalAccel.X = friction;
            }
        }

        protected void ApplyVerticalFriction(float friction)
        {
            if (frictionalAccel.Y < friction)
            {
                frictionalAccel.Y = friction;
            }
        }

        protected void SetHorizontalVelocity(float constVelocity)
        {
            velocity.X = constVelocity;
        }

        protected void SetVerticalVelocity(float constVelocity)
        {
            velocity.Y = constVelocity;
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

        protected override void OnSimulation(int time)
        {
            // Note: if we will support recording/replaying, use a constant number here
            float deltaTime = time / 1000f;

            // Apply conservative accel

            velocity += accel * deltaTime;
            accel.X = 0;
            accel.Y = 0;

            // Apply frictional accel

            Vector2 deltaV = frictionalAccel * deltaTime;

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

            frictionalAccel.X = 0;
            frictionalAccel.X = 0;

            // Apply velocity

            movement += velocity * deltaTime;
            if (velocity.X > Const.VELOCITY_MAX_LR)
            {
                velocity.X = Const.VELOCITY_MAX_LR;
            }
            else if (velocity.X < -Const.VELOCITY_MAX_LR)
            {
                velocity.X = -Const.VELOCITY_MAX_LR;
            }
            if (velocity.Y > Const.VELOCITY_MAX_D)
            {
                velocity.Y = Const.VELOCITY_MAX_D;
            }
            else if (velocity.Y < -Const.VELOCITY_MAX_U)
            {
                velocity.Y = -Const.VELOCITY_MAX_U;
            }

            // Apply movement

            Point pixelMovement = (movement * pixelScale).ToPoint();
            movement -= pixelMovement.ToVector2() / pixelScale;
            Move(pixelMovement);

            base.OnSimulation(time);
        }

        // TODO: make this private again once we have a better collision event dispatch mechanism
        //       a goomba/koopa should "know" in which case it can hurt/bounce mario
        //       instead of doing runtime type-checking on all enemys in mario's class
        protected enum FacingMode
        {
            left,
            right
        }
    }
}

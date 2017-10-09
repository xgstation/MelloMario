using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MelloMario
{
    abstract class BasePhysicalObject : BaseGameObject
    {
        private const float FORCE_G = 10f;
        private const float FORCE_F = 100f;
        private const float VELOCITY_MAX = 6f;

        private float pixelScale;
        private Vector2 movement;
        private Vector2 velocity;
        private Vector2 force;
        private float frictionalForce;

        protected void ApplyForce(Vector2 delta)
        {
            force += delta;
        }

        protected void ApplyGravity(float gravity = FORCE_G)
        {
            ApplyForce(new Vector2(0, gravity));
        }

        protected void ApplyFriction(float friction = FORCE_F)
        {
            frictionalForce += friction;
        }

        protected void Bounce(CollisionMode mode, float rate)
        {
            switch (mode)
            {
                case CollisionMode.Left:
                    if (velocity.X < 0)
                    {
                        movement.X = 0;
                        velocity.X = -velocity.X * rate;
                        StopMoveHorizontal();
                    }
                    break;
                case CollisionMode.Right:
                    if (velocity.X > 0)
                    {
                        movement.X = 0;
                        velocity.X = -velocity.X * rate;
                        StopMoveHorizontal();
                    }
                    break;
                case CollisionMode.Top:
                    if (velocity.Y < 0)
                    {
                        movement.Y = 0;
                        velocity.Y = -velocity.Y * rate;
                        StopMoveVertical();
                    }
                    break;
                case CollisionMode.Bottom:
                    if (velocity.Y > 0)
                    {
                        movement.Y = 0;
                        velocity.Y = -velocity.Y * rate;
                        StopMoveVertical();
                    }
                    break;
            }
        }

        protected void SoftBounce(CollisionMode mode)
        {
            Bounce(mode, 0);
        }

        protected override void OnSimulation(GameTime time)
        {
            // Note: if we will support recording/replaying, use a constant number here
            float deltaTime = (float)time.ElapsedGameTime.TotalMilliseconds / 1000;

            // Apply conservative force

            velocity += force * deltaTime;
            if (velocity.Length() > VELOCITY_MAX)
            {
                velocity.Normalize();
                velocity *= VELOCITY_MAX;
            }

            force.X = 0;
            force.Y = 0;

            // Apply frictional force

            if (velocity.Length() > frictionalForce * deltaTime)
            {
                Vector2 direction = -velocity;
                direction.Normalize();
                velocity += direction * (frictionalForce * deltaTime);
            }
            else
            {
                velocity.X = 0;
                velocity.Y = 0;
            }
            frictionalForce = 0;

            // Apply velocity

            movement += velocity * deltaTime;

            // Apply movement

            Point pixelMovement = (movement * pixelScale).ToPoint();
            movement -= pixelMovement.ToVector2() / pixelScale;
            Move(pixelMovement);
        }

        public BasePhysicalObject(Point location, Point size, float pixelScale) : base(location, size)
        {
            this.pixelScale = pixelScale;
            movement = new Vector2();
            velocity = new Vector2();
            force = new Vector2();
        }
    }
}

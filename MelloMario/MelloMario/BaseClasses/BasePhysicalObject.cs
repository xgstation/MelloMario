using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MelloMario
{
    abstract class BasePhysicalObject : BaseGameObject
    {
        private const float FORCE_G = 20f;
        private const float FORCE_F = 100f;
        private const float VELOCITY_MAX_LR = 10f;
        private const float VELOCITY_MAX_U = 10f;
        private const float VELOCITY_MAX_D = 20f;
        // TODO: add in-air friction

        //note: base on wenli's work:
        //private const float J_FRICTION = 110f;
        //private const float F_FRICTION = 80f;

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
            if (frictionalForce < friction)
            {
                frictionalForce = friction;
            }
        }

        protected void Bounce(CollisionMode mode, float rate)
        {
            switch (mode)
            {
                case CollisionMode.Left:
                case CollisionMode.InnerLeft:
                    if (velocity.X < 0)
                    {
                        movement.X = 0;
                        velocity.X = -velocity.X * rate;
                        StopMoveHorizontal();
                    }
                    break;
                case CollisionMode.Right:
                case CollisionMode.InnerRight:
                    if (velocity.X > 0)
                    {
                        movement.X = 0;
                        velocity.X = -velocity.X * rate;
                        StopMoveHorizontal();
                    }
                    break;
                case CollisionMode.Top:
                case CollisionMode.InnerTop:
                    if (velocity.Y < 0)
                    {
                        movement.Y = 0;
                        velocity.Y = -velocity.Y * rate;
                        StopMoveVertical();
                    }
                    break;
                case CollisionMode.Bottom:
                case CollisionMode.InnerBottom:
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
            float deltaTime = time.ElapsedGameTime.Milliseconds / 1000f;

            // Apply conservative force

            velocity += force * deltaTime;
            force.X = 0;
            force.Y = 0;

            // Apply frictional force

            if (velocity.X > frictionalForce * deltaTime)
            {
                velocity.X -= frictionalForce * deltaTime;
            }
            else if (velocity.X < -frictionalForce * deltaTime)
            {
                velocity.X += frictionalForce * deltaTime;
            }
            else
            {
                velocity.X = 0;
            }
            frictionalForce = 0;

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
        }

        public BasePhysicalObject(IGameWorld world, Point location, Point size, float pixelScale) : base(world, location, size)
        {
            this.pixelScale = pixelScale;
            movement = new Vector2();
            velocity = new Vector2();
            force = new Vector2();
        }
    }
}

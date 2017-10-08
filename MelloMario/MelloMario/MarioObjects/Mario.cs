using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects.DirectionStates;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.MarioObjects.PowerUpStates;
using System;

namespace MelloMario.MarioObjects
{
    class Mario : BaseGameObject
    {
        private IMarioDirectionState directionState;
        private IMarioMovementState movementState;
        private IMarioPowerUpState powerUpState;
        //floatlocx is needed to keep track of movement smaller than one pixel on the screen.
        private float hAccel, hVel, floatLocX;
        // TODO: do movement and state change (sprite change) together
        // TODO: encapsulate the gravity (mushrooms/enemies need them too)
        public float userInX;
        private const float H_FRICTION = 100f;
        public const float H_MAX_ACCEL = 110f;
        private const float H_MAX_VEL = 6f;

        private void OnStateChanged()
        {
            if (powerUpState is PowerUpStandard)
            {
                if (MovementState is Movementldle)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardIdleLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    else if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardIdleRight", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                else if (MovementState is MovementJumping)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardJumpingLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    else if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardJumpingRight", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                else if (MovementState is MovementWalking)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardWalkingLeft", false), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    else if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardWalkingRight", false), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }

            }
            else if (powerUpState is PowerUpFire)
            {
                if (MovementState is MovementCrouching)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireCrouchingLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    else if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireCrouchingRight", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                else if (MovementState is Movementldle)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireIdleLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    else if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireIdleRight", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                else if (MovementState is MovementJumping)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireJumpingLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    else if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireJumpingRight", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                else if (MovementState is MovementWalking)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireWalkingLeft", false), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    else if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireWalkingRight", false), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }

            }
            else if (powerUpState is PowerUpSuper)
            {
                if (MovementState is MovementCrouching)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("SuperCrouchingLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    else if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("SuperCrouchingRight", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                else if (MovementState is Movementldle)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("SuperIdleLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    else if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("SuperIdleRight", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                else if (MovementState is MovementJumping)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("SuperJumpingLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    else if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("SuperJumpingRight", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                else if (MovementState is MovementWalking)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("SuperWalkingLeft", false), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    else if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("SuperWalkingRight", false), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }

            }
        }

        protected override void OnSimulation(GameTime time)
        {
            float oldLocX = floatLocX;
            UpdateHAccel(userInX, time);
            userInX = 0;
            UpdateHVel(time);
            UpdateHPos(time);
            Move(new Point((int)floatLocX - (int)oldLocX, 0));
            //Move(new Point(1, 0));
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode)
        {
        }

        public IMarioDirectionState DirectionState
        {
            get
            {
                return directionState;
            }
            set
            {
                directionState = value;
                OnStateChanged();
            }
        }
        public IMarioMovementState MovementState
        {
            get
            {
                return movementState;
            }
            set
            {
                movementState = value;
                OnStateChanged();
            }
        }
        public IMarioPowerUpState PowerUpState
        {
            get
            {
                return powerUpState;
            }
            set
            {
                powerUpState = value;
                OnStateChanged();
            }
        }

        public Mario(Point location) : base(location, new Point(32, 32))
        {
            directionState = new DirectionRight(this);
            movementState = new Movementldle(this);
            powerUpState = new PowerUpStandard(this);
            hAccel = 0;
            hVel = 0;
            userInX = 0;
            floatLocX = location.X;
            OnStateChanged();
        }

        public void UpdateHPos(GameTime time)
        {
            floatLocX += (hVel * Boundary.Width) * (float)time.ElapsedGameTime.TotalMilliseconds / 1000;
        }

        public void UpdateHVel(GameTime time)
        {
            float oldVel = hVel;
            hVel += hAccel * (float)time.ElapsedGameTime.TotalMilliseconds / 1000;
            if (hVel > H_MAX_VEL)
                hVel = H_MAX_VEL;
            else if (hVel < -1 * H_MAX_VEL)
                hVel = -1 * H_MAX_VEL;
            if (oldVel * hVel < 0)
            {
                hAccel = 0;
                hVel = 0;
            }
        }

        public void UpdateHAccel(float userDelta, GameTime time)
        {

            if (hVel > 0)
            {
                hAccel -= H_FRICTION * (float)time.ElapsedGameTime.TotalMilliseconds / 1000;
            }
            else if (hVel < 0)
            {
                hAccel += H_FRICTION * (float)time.ElapsedGameTime.TotalMilliseconds / 1000;
            }
            hAccel += userDelta * (float)time.ElapsedGameTime.TotalMilliseconds / 1000;
            if (hAccel > H_MAX_ACCEL)
            {
                hAccel = H_MAX_ACCEL;
            }
            else if (hAccel < -1 * H_MAX_ACCEL)
            {
                hAccel = -1 * H_MAX_ACCEL;
            }
        }

        public void TurnLeft()
        {
            DirectionState.TurnLeft();
        }
        public void TurnRight()
        {
            DirectionState.TurnRight();
        }
        public void Crouch()
        {
            MovementState.Crouch();
        }
        public void Idle()
        {
            MovementState.Idle();
        }
        public void Jump()
        {
            MovementState.Jump();
        }
        public void Walk()
        {
            MovementState.Walk();
        }
        public void UpgradeToSuper()
        {
            PowerUpState.UpgradeToSuper();
        }
        public void UpgradeToFire()
        {
            PowerUpState.UpgradeToFire();
        }
        public void Downgrade()
        {
            PowerUpState.Downgrade();
        }
        public void Kill()
        {
            PowerUpState.Kill();
        }
    }
}

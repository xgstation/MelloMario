using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects.DirectionStates;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.MarioObjects.PowerUpStates;

namespace MelloMario.MarioObjects
{
    class Mario : BaseGameObject
    {
        private IMarioDirectionState directionState;
        private IMarioMovementState movementState;
        private IMarioPowerUpState powerUpState;

        private void OnStateChanged()
        {
            if (powerUpState is PowerUpStandard)
            {
                if(MovementState is Movementldle)
                {
                    if(directionState is DirectionLeft)
                    {
                         ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardIdleLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardIdleRight", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                 if(MovementState is MovementJumping)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardJumpingLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardJumpingRight", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                 if(MovementState is MovementWalking)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardWalkingLeft", false), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardWalkingRight", false), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
            
            }
            if (powerUpState is PowerUpFire)
            {
                if(MovementState is MovementCrouching)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireCrouchingLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireCrouchingRight", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                if (MovementState is Movementldle)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireIdleLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireIdleRight", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                if (MovementState is MovementJumping)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireJumpingLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireJumpingRight", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                if (MovementState is MovementWalking)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireWalkingLeft", false), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireWalkingRight", false), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }

            }
            if (powerUpState is PowerUpSuper)
            {
                if (MovementState is MovementCrouching)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("SuperCrouchingLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("SuperCrouchingRight", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                if (MovementState is Movementldle)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("SuperIdleLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("SuperIdleRight", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                if (MovementState is MovementJumping)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("SuperJumpingLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("SuperJumpingRight", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                if (MovementState is MovementWalking)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("SuperWalkingLeft", false), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    if (directionState is DirectionRight)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("SuperWalkingRight", false), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }

            }
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target)
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
            OnStateChanged();
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

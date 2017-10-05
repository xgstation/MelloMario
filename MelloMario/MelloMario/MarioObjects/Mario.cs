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
                         ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardIdleLeft", false), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    else
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardIdleRight", false), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                else if(MovementState is MovementJumping)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardJumpingLeft", false), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    else
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardJumpingRight", false), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                }
                else if(MovementState is MovementWalking)
                {
                    if (directionState is DirectionLeft)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardWalkingLeft", true), ResizeModeX.Center, ResizeModeY.Bottom);
                    }
                    else
                    {
                        ShowSprite(SpriteFactory.Instance.CreateMarioSprite("StandardWalkingWalking", true), ResizeModeX.Center, ResizeModeY.Bottom);
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

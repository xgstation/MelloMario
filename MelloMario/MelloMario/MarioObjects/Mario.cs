using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects.DirectionStates;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.MarioObjects.PowerUpStates;
using MelloMario.MarioObjects.ProtectionStates;

namespace MelloMario.MarioObjects
{
    class Mario : BasePhysicalObject, IGameCharacter
    {
        private IMarioDirectionState directionState;
        private IMarioMovementState movementState;
        private IMarioPowerUpState powerUpState;
        private IMarioProtectionState protectionState;
        // TODO: encapsulate user input
        private Vector2 userInput;

        private void OnStateChanged()
        {
            if (powerUpState is Dead)
            {
                ShowSprite(SpriteFactory.Instance.CreateMarioSprite(powerUpState.GetType().Name, true));
            }
            else
            {
                ShowSprite(SpriteFactory.Instance.CreateMarioSprite(
                    powerUpState.GetType().Name + movementState.GetType().Name + directionState.GetType().Name,
                    !(movementState is Walking)
                ));
            }
        }

        protected override void OnSimulation(GameTime time)
        {
            ApplyForce(userInput);
            userInput.X = 0;
            userInput.Y = 0;
            ApplyGravity();

            base.OnSimulation(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode)
        {
            // TODO: if target is block
            if (mode == CollisionMode.Bottom)
            {
                ApplyFriction();
            }
            SoftBounce(mode);
        }

        protected override void OnOut(CollisionMode mode)
        {
            SoftBounce(mode);
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
        public IMarioProtectionState ProtectionState
        {
            get
            {
                return protectionState;
            }
            set
            {
                protectionState = value;
                OnStateChanged();
            }
        }

        public Mario(IGameWorld world, Point location) : base(world, location, new Point(32, 32), 32)
        {
            directionState = new Right(this);
            movementState = new Standing(this);
            powerUpState = new Standard(this);
            protectionState = new Protected(this);
            OnStateChanged();
        }

        public void Left()
        {
            userInput.X -= 110;
            DirectionState.TurnLeft();
        }
        public void Right()
        {
            userInput.X += 110;
            DirectionState.TurnRight();
        }

        public void Jump()
        {
            userInput.Y -= 110;
            MovementState.Jump();
        }

        public void Crouch()
        {
            MovementState.Crouch();
        }

        public void Action()
        {
            throw new System.NotImplementedException();
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
            // TODO
        }
    }
}

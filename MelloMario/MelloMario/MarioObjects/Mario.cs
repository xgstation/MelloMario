using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.MarioObjects.PowerUpStates;
using MelloMario.MarioObjects.ProtectionStates;

namespace MelloMario.MarioObjects
{
    class Mario : BasePhysicalObject, IGameCharacter
    {
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
                string facingString;
                if (facing == Facing.left)
                {
                    facingString = "Left";
                }
                else
                {
                    facingString = "Right";
                }

                ShowSprite(SpriteFactory.Instance.CreateMarioSprite(
                    powerUpState.GetType().Name + movementState.GetType().Name + facingString,
                    !(movementState is Walking)
                ));
            }
        }

        protected override void OnSimulation(GameTime time)
        {
            ApplyForce(userInput);
            userInput.X = 0;
            userInput.Y = 0;

            if (g_on)
            {
                ApplyGravity();
            }
            ApplyHorizontalFriction(FORCE_F_AIR);
            ApplyVerticalFriction(FORCE_F_AIR);

            base.OnSimulation(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode)
        {
            // TODO: if target is block
            if (mode == CollisionMode.Bottom)
            {
                ApplyHorizontalFriction(FORCE_F_GROUND);
            }
            SoftBounce(mode);
        }

        protected override void OnOut(CollisionMode mode)
        {
            SoftBounce(mode);
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
            movementState = new Standing(this);
            powerUpState = new Standard(this);
            protectionState = new Protected(this);
            OnStateChanged();
        }

        public void Left()
        {
            userInput.X -= FORCE_MOVE;
            facing = Facing.left;
            OnStateChanged();
        }
        public void Right()
        {
            userInput.X += FORCE_MOVE;
            facing = Facing.right;
            OnStateChanged();
        }

        public void Jump()
        {
            userInput.Y -= FORCE_JUMP;
            if (g_on)
            {
                userInput.Y -= FORCE_G;
            }
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

        // TODO: For sprint 2 only
        //       Please remove it after sprint 2
        [System.Obsolete]
        bool g_on;
        [System.Obsolete]
        public void ToggleGravity()
        {
            g_on = !g_on;
        }
        [System.Obsolete]
        public void Fall()
        {
            userInput.Y += FORCE_MOVE;
        }
    }
}

using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.MarioObjects.PowerUpStates;
using MelloMario.MarioObjects.ProtectionStates;
using MelloMario.BlockObjects;
using MelloMario.EnemyObjects;
using System;

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
            if (movementState is Crouching && powerUpState is Standard)
            {
                movementState.Idle();
            }
            else if (powerUpState is Dead)
            {
                ShowSprite(SpriteFactory.Instance.CreateMarioSprite(powerUpState.GetType().Name, true));
            }
            else
            {
                string facingString;
                if (Facing == FacingMode.left)
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

        private void ChangeFacing(FacingMode facing)
        {
            Facing = facing;

            // Notice: The effect should be the same as changing Mario's state
            OnStateChanged();
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
            switch (target.GetType().Name)
            {
                case "Brick":
                    //TODO:Hidden Brick
                case "Floor":
                case "Pipeline":
                case "Question":
                    //TODO:Hidden Question
                case "Stair":
                    SoftBounce(mode);

                    if (mode == CollisionMode.Bottom)
                    {
                        ApplyHorizontalFriction(FORCE_F_GROUND);

                        if (MovementState is Jumping)
                        {
                            MovementState.Idle();
                        }
                    }

                    break;
                case "Goomba":
                    if (mode != CollisionMode.Bottom)
                    {
                        if (((Goomba)target).State is EnemyObjects.GoombaStates.Normal)
                        {
                            PowerUpState.Downgrade();
                        }
                    }

                    break;
                case "Koopa":
                    if (mode != CollisionMode.Bottom)
                    {
                        if (((Koopa)target).State is EnemyObjects.KoopaStates.Normal)
                        {
                            PowerUpState.Downgrade();
                        }
                    }

                    break;
                case "Coin":
                    // TODO: coin +1
                    break;
                case "FireFlower":
                    PowerUpState.UpgradeToFire();
                    break;
                case "OneUpMushroom":
                    // TODO: life +1
                    break;
                case "Star":
                    ProtectionState.Star();
                    break;
                case "SuperMushroom":
                    PowerUpState.UpgradeToSuper();
                    break;
            }
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
            if (!(MovementState is Crouching))
            {
                userInput.X -= FORCE_INPUT;
            }
            if (Facing == FacingMode.right)
            {
                ChangeFacing(FacingMode.left);
            }
            if (MovementState is Standing)
            {
                MovementState.Walk();
            }
        }

        public void LeftRelease()
        {
            if (MovementState is Walking)
            {
                MovementState.Idle();
            }
        }

        public void Right()
        {
            if (!(MovementState is Crouching))
            {
                userInput.X += FORCE_INPUT;
            }
            if (Facing == FacingMode.left)
            {
                ChangeFacing(FacingMode.right);
            }
            if (MovementState is Standing)
            {
                MovementState.Walk();
            }
        }

        public void RightRelease()
        {
            if (MovementState is Walking)
            {
                MovementState.Idle();
            }
        }

        public void Jump()
        {
            if (MovementState is Crouching)
            {
                // TODO: for sprint2 only
                SoftBounce(CollisionMode.Bottom);
            }
            else
            {
                userInput.Y -= FORCE_INPUT;
                if (g_on)
                {
                    userInput.Y -= FORCE_G;
                }
            }

            MovementState.Jump();
        }

        public void Crouch()
        {
            if (MovementState is Jumping)
            {
                // TODO: for sprint2 only
                SoftBounce(CollisionMode.Top);
            }
            else
            {
                // TODO: for sprint2 only
                // "fall" instead of "crouch"
                userInput.Y += FORCE_INPUT;          
            }

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
        bool g_on;
        public void ToggleGravity()
        {
            g_on = !g_on;
        }

        public void LeftPress()
        {
            throw new NotImplementedException();
        }

        public void RightPress()
        {
            throw new NotImplementedException();
        }

        public void JumpRelease()
        {
            throw new NotImplementedException();
        }

        public void JumpPress()
        {
            throw new NotImplementedException();
        }

        public void CrouchRelease()
        {
            throw new NotImplementedException();
        }

        public void CrouchPress()
        {
            throw new NotImplementedException();
        }
    }
}

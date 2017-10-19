using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.MarioObjects.PowerUpStates;
using MelloMario.MarioObjects.ProtectionStates;
using MelloMario.BlockObjects;
using MelloMario.EnemyObjects;

namespace MelloMario.MarioObjects
{
    class Mario : BasePhysicalObject
    {
        private IMarioMovementState movementState;
        private IMarioPowerUpState powerUpState;
        private IMarioProtectionState protectionState;

        private void UpdateSprite()
        {
            if (movementState is Crouching && powerUpState is Standard)
            {
                movementState.Idle();
            }
            else if (protectionState is Dead)
            {
                ShowSprite(SpriteFactory.Instance.CreateMarioSprite(protectionState.GetType().Name, true));
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

        protected void ChangeFacing(FacingMode facing)
        {
            Facing = facing;

            // Notice: The effect should be the same as changing state
            UpdateSprite();
        }

        protected override void OnSimulation(GameTime time)
        {
            if (g_on)
            {
                ApplyGravity();
            }
            ApplyHorizontalFriction(FORCE_F_AIR);
            ApplyVerticalFriction(FORCE_F_AIR);

            base.OnSimulation(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionCornerMode corner, CollisionCornerMode cornerPassive)
        {
            switch (target.GetType().Name)
            {
                //TODO: add cases when collision mode is OutLeftTop/OutRightTop that Mario will be squeezed to side of the brick
                //NOTE: if collision mode is OutLeftTop/OutRightTop, be careful that if there are blocks surronding the target, no squeeze will happen!!
                case "Brick":
                case "Question":
                    bool isHidden;
                    bool isBumping; // TODO: remove later

                    if (target is Brick brick)
                    {
                        isHidden = brick.State is BlockObjects.BrickStates.Hidden;
                        isBumping = brick.State is BlockObjects.BrickStates.Bumped;
                    }
                    else if (target is Question question)
                    {
                        isHidden = question.State is BlockObjects.QuestionStates.Hidden;
                        isBumping = question.State is BlockObjects.QuestionStates.Bumped;
                    }
                    else
                    {
                        // never reach
                        isHidden = false;
                        isBumping = false;
                    }

                    if (!isHidden || (mode == CollisionMode.Top && corner == CollisionCornerMode.Center))
                    {
                        // TODO: hack
                        if (isBumping && mode == CollisionMode.Top && corner == CollisionCornerMode.Center)
                        {
                            Bounce(mode, new Vector2(), 1);
                            Move(new Point(0, 7));
                        }
                        goto case "Stair";
                    }

                    break;
                case "Floor":
                case "Pipeline":
                case "Stair":
                    Bounce(mode, new Vector2());

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
                    if (target is Goomba goomba && mode != CollisionMode.Bottom)
                    {
                        if (goomba.State is EnemyObjects.GoombaStates.Normal && !(ProtectionState is Starred))
                        {
                            PowerUpState.Downgrade();
                        }
                    }

                    break;
                case "Koopa":
                    if (target is Koopa koopa && mode != CollisionMode.Bottom)
                    {
                        if (koopa.State is EnemyObjects.KoopaStates.Normal && !(ProtectionState is Starred))
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
            Bounce(mode, new Vector2());
        }

        protected override void OnDraw(GameTime time)
        {
            if (ProtectionState is Starred)
            {
                if (time.TotalGameTime.Milliseconds % 8 == 0)
                {
                    HideSprite();
                }
                else
                {
                    UpdateSprite();
                }
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
                UpdateSprite();
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
                UpdateSprite();
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
                UpdateSprite();
            }
        }

        public Mario(IGameWorld world, Point location) : base(world, location, new Point(32, 32), 32)
        {
            movementState = new Standing(this);
            powerUpState = new Standard(this);
            protectionState = new Normal(this);
            UpdateSprite();
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

        // TODO: For sprint 2 only
        //       Please remove it after sprint 2
        bool g_on;
        public void ToggleGravity()
        {
            g_on = !g_on;
        }
    }
}

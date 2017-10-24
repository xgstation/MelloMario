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

        protected override void OnUpdate(GameTime time)
        {
            ApplyGravity();
            ApplyHorizontalFriction(FORCE_F_AIR);
            ApplyVerticalFriction(FORCE_F_AIR);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
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

                    // if (mode == CollisionMode.Top && corner == CollisionCornerMode.Center) // TODO
                    if (mode == CollisionMode.Top)
                    {
                        if (Bounce(mode, new Vector2(), 1))
                        {
                            if (isBumping)
                            {
                                Move(new Point(0, 1));
                            }
                            else
                            {
                                if (target is Brick brick1)
                                {
                                    brick1.Bump(this);
                                }
                                else if (target is Question question1)
                                {
                                    question1.Bump(this);
                                }
                            }
                        }
                    }
                    else if (!isHidden)
                    {
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
                    if (((ItemObjects.Coin)target).State is ItemObjects.CoinStates.Normal) { }
                    // TODO: implement coins count
                    break;
                case "FireFlower":
                    if (((ItemObjects.FireFlower)target).State is ItemObjects.FireFlowerStates.Normal)
                        PowerUpState.UpgradeToFire();
                    break;
                case "OneUpMushroom":
                    if (((ItemObjects.OneUpMushroom)target).State is ItemObjects.OneUpMushroomStates.Normal) { }
                    // TODO: implement +1s
                    break;
                case "Star":
                    if (((ItemObjects.Star)target).State is ItemObjects.StarStates.Normal)
                        ProtectionState.Star();
                    break;
                case "SuperMushroom":
                    if (((ItemObjects.SuperMushroom)target).State is ItemObjects.SuperMushroomStates.Normal)
                        PowerUpState.UpgradeToSuper();
                    break;
            }
        }

        protected override void OnOut(CollisionMode mode)
        {
            Bounce(mode, new Vector2());
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
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

    }
}

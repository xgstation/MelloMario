namespace MelloMario.Objects.Characters
{
    #region

    using System;
    using System.Diagnostics.CodeAnalysis;
    using MelloMario.Factories;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Blocks.BrickStates;
    using MelloMario.Objects.Characters.MovementStates;
    using MelloMario.Objects.Characters.PowerUpStates;
    using MelloMario.Objects.Characters.ProtectionStates;
    using MelloMario.Objects.Enemies;
    using MelloMario.Objects.Items;
    using MelloMario.Sounds.Effects;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;


    #endregion


    internal class Mario : BasePhysicalObject, ISoundable
    {
        public delegate void GameOverHandler(Mario m, EventArgs e);
        private IMarioMovementState movementState;
        private IMarioPowerUpState powerUpState;
        private IMarioProtectionState protectionState;
        private Beetle helmet;

        public Mario(
            IWorld world,
            Point location,
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener) : base(
            world,
            location,
            listener,
            new Point(),
            32)
        {
            listener.Subscribe(this);
            soundListener.Subscribe(this);
            SoundEventArgs = new SoundArgs();
            powerUpState = new Standard(this);
            movementState = new Standing(this);
            protectionState = new ProtectionStates.Normal(this);
            UpdateSprite();
        }

        public ISoundArgs SoundEventArgs { get; }

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

        public void WearHelmet(Beetle beetle)
        {
            ; helmet = beetle;
            protectionState.Helmet();
        }

        public void LoseHelmet()
        {
            helmet.Defeat();
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

        public event SoundHandler SoundEvent;
        public event GameOverHandler HandlerGameOver;

        public void OnDeath()
        {
            SoundEventArgs.SetMethodCalled();
            SetVerticalVelocity(-20);
        }

        public void TransToGameOver()
        {
            HandlerGameOver?.Invoke(this, EventArgs.Empty);
        }

        public void UpgradeToSuper()
        {
            SoundEventArgs.SetMethodCalled();
            PowerUpState.UpgradeToSuper();
        }

        public void UpgradeToFire()
        {
            SoundEventArgs.SetMethodCalled();
            PowerUpState.UpgradeToFire();
        }

        public void Downgrade()
        {
            if (protectionState is Helmeted)
            {
                protectionState.Protect();
            }
            if (protectionState is ProtectionStates.Normal)
            {
                PowerUpState.Downgrade();
            }
            if (protectionState is ProtectionStates.Normal)
            {
                protectionState.Protect();
            }
        }

  

        protected void ChangeFacing(FacingMode facing)
        {
            Facing = facing;

            // Notice: The effect should be the same as changing state
            UpdateSprite();
        }

        protected override void OnUpdate(int time)
        {
            powerUpState.Update(time);
            movementState.Update(time);
            protectionState.Update(time);
        }

        protected override void OnSimulation(int time)
        {
            SoundEvent?.Invoke(this, SoundEventArgs);
            ApplyGravity();
            ApplyHorizontalFriction(Const.ACCEL_F_AIR);
            ApplyVerticalFriction(Const.ACCEL_F_AIR);

            base.OnSimulation(time);
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override void OnCollision(
            IGameObject target,
            CollisionMode mode,
            CollisionMode modePassive,
            CornerMode corner,
            CornerMode cornerPassive)
        {
            if (ProtectionState is Dead)
            {
                return;
            }
            switch (target.GetType().Name)
            {
                //TODO: add cases when collision mode is OutLeftTop/OutRightTop that Mario will be squeezed to side of the brick
                //NOTE: if collision mode is OutLeftTop/OutRightTop, be careful that if there are blocks surronding the target, no squeeze will happen!!
                case "Brick":
                case "Question":
                    bool isHidden;
                    bool isBumping;

                    Brick brick = target as Brick;
                    Question question = target as Question;

                    if (brick != null)
                    {
                        if (brick.State is Destroyed)
                        {
                            break;
                        }
                        isHidden = brick.State is Hidden;
                        isBumping = brick.State is Bumped;
                    }
                    else if (question != null)
                    {
                        isHidden = question.State is Blocks.QuestionStates.Hidden;
                        isBumping = question.State is Blocks.QuestionStates.Bumped;
                    }
                    else
                    {
                        // never reach
                        isHidden = false;
                        isBumping = false;
                    }

                    if (mode == CollisionMode.Top)
                    {
                        bool bumped = Bounce(mode, new Vector2(), 1);
                        if (isBumping)
                        {
                            //Move(new Point(0, 1));
                        }
                        else if (bumped)
                        {
                            if (brick != null)
                            {
                                brick.Bump(this);
                            }
                            else if (question != null)
                            {
                                question.Bump(this);
                            }
                        }
                    }
                    else if (!isHidden)
                    {
                        goto case "Stair";
                    }

                    break;
                case "Pipeline":
                    if (MovementState is Crouching && Database.IsEntrance((Pipeline) target))
                    {
                        string type = (target as Pipeline).Type;
                        if (type == "LeftIn")
                        {
                            if (Boundary.Center.X > target.Boundary.Center.X)
                            {
                                Bounce(CollisionMode.Left, new Vector2());
                                Bounce(CollisionMode.Right, new Vector2());
                            }
                            else
                            {
                                goto case "Floor";
                            }
                        }
                        else if (type == "RightIn")
                        {
                            if (Boundary.Center.X < target.Boundary.Center.X)
                            {
                                Bounce(CollisionMode.Left, new Vector2());
                                Bounce(CollisionMode.Right, new Vector2());
                            }
                            else
                            {
                                goto case "Floor";
                            }
                        }
                    }
                    else
                    {
                        goto case "Floor";
                    }
                    break;
                case "CompressedBlock":
                case "Floor":
                case "Stair":
                case "Flag":
                    Bounce(mode, new Vector2());
                    if (mode == CollisionMode.Bottom)
                    {
                        ApplyHorizontalFriction(Const.ACCEL_F_GROUND);
                        MovementState.Land();
                    }
                    break;
                case "Goomba":
                    if (target is Goomba goomba && mode != CollisionMode.Bottom)
                    {
                        if (goomba.State is Enemies.GoombaStates.Normal && !(ProtectionState is Starred))
                        {
                            Downgrade();
                        }
                    }
                    else if (!(protectionState is Starred))
                    {
                        Bounce(mode, new Vector2(), 0.5f);
                    }

                    break;
                case "Thwomp":
                    if (target is Thwomp)
                    {
                        if (!(ProtectionState is Starred))
                        {
                            Downgrade();
                        }
                    }
                    break;
                case "Beetle":
                    if (target is Beetle)
                    {
                        if (!(ProtectionState is Starred))
                        {
                            if (mode is CollisionMode.Bottom)
                            {
                                if (corner is CornerMode.Right)
                                {
                                    Bounce(mode, new Vector2(1f, -5f), 1f);
                                }
                                else if (corner is CornerMode.Left)
                                {
                                    Bounce(mode, new Vector2(-1f, -5f), 1f);
                                }
                                else
                                {
                                    Bounce(mode, new Vector2(0, -5f), 1f);
                                }
                            }
                        }
                    }
                    break;
                case "Koopa":
                    if (target is Koopa)
                    {
                        if (!(ProtectionState is Starred))
                        {
                            if (mode is CollisionMode.Bottom)
                            {
                                if (corner is CornerMode.Right)
                                {
                                    Bounce(mode, new Vector2(1f, -5f), 1f);
                                }
                                else if (corner is CornerMode.Left)
                                {
                                    Bounce(mode, new Vector2(-1f, -5f), 1f);
                                }
                                else
                                {
                                    Bounce(mode, new Vector2(0, -5f), 1f);
                                }
                            }
                        }
                    }
                    break;
                case "FireFlower":
                    if (((FireFlower) target).State is Items.FireFlowerStates.Normal)
                    {
                        UpgradeToFire();
                    }
                    break;
                case "Star":
                    if (((Star) target).State is Items.StarStates.Normal)
                    {
                        ProtectionState.Star();
                    }
                    break;
                case "SuperMushroom":
                    if (((SuperMushroom) target).State is Items.SuperMushroomStates.Normal && PowerUpState is Standard)
                    {
                        UpgradeToSuper();
                    }
                    break;
                case "Piranha":
                    if (!(ProtectionState is Starred))
                    {
                        if (!(((Piranha) target).State is Enemies.PiranhaStates.Hidden))
                        {
                            Downgrade();
                        }
                    }
                    break;
            }
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
            if (!(protectionState is Dead))
            {
                Bounce(mode, new Vector2());
                if (mode == CollisionMode.InnerBottom)
                {
                    ProtectionState = new Dead(this);
                }
            }
        }

        private void UpdateSprite()
        {
            if (movementState is Crouching && powerUpState is Standard)
            {
                return; // status is still updating
            }

            string facingString;
            if (Facing == FacingMode.left)
            {
                facingString = "Left";
            }
            else
            {
                facingString = "Right";
            }

            ShowSprite(
                SpriteFactory.Instance.CreateMarioSprite(
                    powerUpState.GetType().Name,
                    movementState.GetType().Name,
                    protectionState.GetType().Name,
                    facingString));
        }
    }
}

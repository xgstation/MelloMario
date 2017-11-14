﻿using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects.MovementStates;
using MelloMario.MarioObjects.PowerUpStates;
using MelloMario.MarioObjects.ProtectionStates;
using MelloMario.BlockObjects;
using MelloMario.EnemyObjects;
using MelloMario.Interfaces.Objects.States;
using MelloMario.Theming;

namespace MelloMario.MarioObjects
{
    class Mario : BasePhysicalObject
    {
        private IMarioPowerUpState powerUpState;
        private IMarioMovementState movementState;
        private IMarioProtectionState protectionState;

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

            ShowSprite(SpriteFactory.Instance.CreateMarioSprite(
                powerUpState.GetType().Name, movementState.GetType().Name, protectionState.GetType().Name, facingString
            ));
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
            ApplyGravity();
            ApplyHorizontalFriction(GameConst.FORCE_F_AIR);
            ApplyVerticalFriction(GameConst.FORCE_F_AIR);

            base.OnSimulation(time);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
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
                        if (brick.State is BlockObjects.BrickStates.Destroyed)
                        {
                            break;
                        }
                        isHidden = brick.State is BlockObjects.BrickStates.Hidden;
                        isBumping = brick.State is BlockObjects.BrickStates.Bumped;
                    }
                    else if (question != null)
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
                    if (MovementState is Crouching && GameDatabase.IsEntrance((Pipeline) target))
                    {
                        string type = (target as Pipeline).Type;
                        if (type == "LeftIn")
                        {
                            if (Boundary.Center.X > target.Boundary.Center.X)
                            {
                                Move(new Point(0, 3));
                                StopHorizontalMovement();
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
                                Move(new Point(0, 3));
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
                    Bounce(mode, new Vector2());
                    if (mode == CollisionMode.Bottom)
                    {
                        ApplyHorizontalFriction(GameConst.FORCE_F_GROUND);
                        MovementState.Land();
                    }

                    break;
                case "Flag":
                    
                    break;
                case "Goomba":
                    if (target is Goomba goomba && mode != CollisionMode.Bottom)
                    {
                        if (goomba.State is EnemyObjects.GoombaStates.Normal && !(ProtectionState is Starred))
                        {
                            Downgrade();
                        }
                    }
                    else if (!(protectionState is Starred))
                    {
                        Bounce(mode, new Vector2(), 0.5f);
                    }

                    break;
                case "Koopa":
                    //if (target is Koopa koopa && mode != CollisionMode.Bottom)
                    //{
                    //    if (koopa.State is EnemyObjects.KoopaStates.Normal && !(ProtectionState is Starred))
                    //    {
                    //        Downgrade();
                    //    }
                    //    else if (koopa.State is EnemyObjects.KoopaStates.MovingShell && !(ProtectionState is Starred))
                    //    {
                    //        if(koopa.Facing is FacingMode.right && (mode is CollisionMode.InnerRight || mode is CollisionMode.Left))
                    //            Downgrade();
                    //        if (koopa.Facing is FacingMode.left && (mode is CollisionMode.InnerLeft || mode is CollisionMode.Right))
                    //            Downgrade();
                    //    }
                    //}
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
                            else if (mode is CollisionMode.Left || mode is CollisionMode.Right)
                            {
                                Bounce(mode, new Vector2(), -0.5f);
                            }
                        }
                    }

                    //else if (!(protectionState is Starred))
                    //{
                    //    Bounce(mode, new Vector2(0, -5f), 1f);
                    //}

                    break;
                case "FireFlower":
                    if (((ItemObjects.FireFlower) target).State is ItemObjects.FireFlowerStates.Normal)
                    {
                        UpgradeToFire();
                    }
                    break;
                case "Star":
                    if (((ItemObjects.Star) target).State is ItemObjects.StarStates.Normal)
                    {
                        ProtectionState.Star();
                    }
                    break;
                case "SuperMushroom":
                    if (((ItemObjects.SuperMushroom) target).State is ItemObjects.SuperMushroomStates.Normal &&
                        PowerUpState is Standard)
                    {
                        UpgradeToSuper();
                    }
                    break;
                case "Piranha":
                    if (!(ProtectionState is Starred))
                    {
                        if (!(((Piranha)target).State is EnemyObjects.PiranhaStates.Hidden))
                        {
                            Downgrade();
                        }
                    }
                    break;
            }
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode)
        {
        }

        protected override void OnCollideWorld(CollisionMode mode)
        {
            
            Bounce(mode, new Vector2());
            if(mode == CollisionMode.InnerBottom)
            {
                KillWithBypass();
            }
        }

        public void OnDeath()
        {
            ChangeLives(-1);
        }

        protected override void OnDraw(int time, Rectangle viewport)
        {
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

        public Mario(IGameWorld world, Point location, Listener listener) : base(world, location, listener, new Point(), 32)
        {
            powerUpState = new Standard(this);
            movementState = new Standing(this);
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

        public void KillWithBypass()
        {
            PowerUpState.Downgrade();
            PowerUpState.Downgrade();
            PowerUpState.Downgrade();
            PowerUpState.Downgrade();
        }

        public void Downgrade()
        {
            if (protectionState is Normal)
            {
                PowerUpState.Downgrade();
            }
            if (protectionState is Normal)
            {
                protectionState.Protect();
            }
        }
    }
}

﻿namespace MelloMario.Objects.Enemies
{
    #region

    using System;
    using MelloMario.Factories;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Blocks.BrickStates;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Characters.ProtectionStates;
    using MelloMario.Objects.Items;
    using MelloMario.Objects.Miscs;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using MelloMario.Objects.Enemies.BeetleStates;
    using MelloMario.Objects.Characters.MovementStates;

    #endregion

    internal class Beetle : BasePhysicalObject
    {
        private IBeetleState state;
        private Mario helmetParent;

        public Beetle(IWorld world, Point location, IListener<IGameObject> listener) :
            base(world, location, listener, new Point(32, 32), 32)
        {
            state = new BeetleStates.Normal(this);
            UpdateSprite();
        }

        public IBeetleState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                UpdateSprite();
            }
        }

        public void JumpOn()
        {
            ScorePoints(Const.SCORE_BEETLE);
            State.JumpOn();
        }

        public void Pushed()
        {
            // TODO: temporary fix
            if (Facing == FacingMode.right)
            {
                Move(new Point(1, 0));
            }
            else
            {
                Move(new Point(-1, 0));
            }

            State.Pushed();
        }

        public void Defeat()
        {
            ScorePoints(Const.SCORE_BEETLE);
            World.Add(new PopingUpPoints(World, Boundary.Location, Const.SCORE_KOOPA));
            RemoveSelf();
        }

        public void Wear(Mario mario)
        {
            helmetParent = mario;
            State.Wear();
            mario.WearHelmet(this);

        }

        protected override void OnUpdate(int time)
        {
            state.Update(time);
        }

        protected override void OnSimulation(int time)
        {
            if (!(state is Worn))
            {
                ApplyGravity();
            }

            if (state is MovingShell || state is NewlyMovingShell)
            {
                if (Facing == FacingMode.left)
                {
                    SetHorizontalVelocity(-Const.VELOCITY_BEETLE_SHELL);
                }
                else
                {
                    SetHorizontalVelocity(Const.VELOCITY_BEETLE_SHELL);
                }
            }
            else if (state is BeetleStates.Normal)
            {
                if (Facing == FacingMode.left)
                {
                    SetHorizontalVelocity(-Const.VELOCITY_BEETLE);
                }
                else
                {
                    SetHorizontalVelocity(Const.VELOCITY_BEETLE);
                }
            }
            else if (state is Worn)
            {
                StopHorizontalMovement();
                StopVerticalMovement();
                Relocate(new Point(helmetParent.Location.X - 5, helmetParent.Location.Y - 5));
            }
            else
            {
                SetHorizontalVelocity(0);
            }

            base.OnSimulation(time);
        }

        protected override void OnCollision(
            IGameObject target,
            CollisionMode mode,
            CollisionMode modePassive,
            CornerMode corner,
            CornerMode cornerPassive)
        {
            if (!(state is Worn))
            {
                switch (target)
                {
                    case Mario mario:
                        OnCollisionMario(mario, mode, corner);
                        break;
                    case Brick brick when brick.State is Hidden:
                        break;
                    case Question question when question.State is Blocks.QuestionStates.Hidden:
                        break;
                    case IGameObject _ when target is Brick
                    || target is Question
                    || target is Floor
                    || target is Pipeline
                    || target is Stair:
                        if (mode == CollisionMode.Left)
                        {
                            Bounce(mode, new Vector2(), 1);
                            ChangeFacing(FacingMode.right);
                        }
                        else if (mode == CollisionMode.Right)
                        {
                            Bounce(mode, new Vector2(), 1);
                            ChangeFacing(FacingMode.left);
                        }
                        else if (mode == CollisionMode.Bottom)
                        {
                            Bounce(mode, new Vector2());
                        }
                        break;
                    case FireBall _:
                        Defeat();
                        break;
                    case Koopa koopa:
                        if (koopa.State is KoopaStates.MovingShell || koopa.State is KoopaStates.NewlyMovingShell)
                        {
                            Defeat();
                        }
                        break;
                    case Beetle beetle:
                        if (beetle.State is MovingShell || beetle.State is NewlyMovingShell)
                        {
                            Defeat();
                        }
                        break;
                }
            }
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        private void OnCollisionMario(Mario mario, CollisionMode mode, CornerMode corner)
        {
            if (mario.MovementState is Crouching && !(mario.ProtectionState is Starred) && (state is MovingShell || state is NewlyMovingShell))
            {
                Wear(mario);
            }
            else if (mario.ProtectionState is Starred)
            {
                Defeat();
            }
            else if ((state is BeetleStates.Normal || state is MovingShell) && !(mario.ProtectionState is Dead))
            {
                if (mode == CollisionMode.Top)
                {
                    JumpOn();
                }
                else
                {
                    mario.Downgrade();
                }
            }
            else if (state is Defeated && !(mario.ProtectionState is Dead))
            {
                switch (mode)
                {
                    case CollisionMode.Left:
                        ChangeFacing(FacingMode.right);
                        Pushed();
                        break;
                    case CollisionMode.Right:
                        ChangeFacing(FacingMode.left);
                        Pushed();
                        break;
                    case CollisionMode.Top when corner == CornerMode.Left:
                        ChangeFacing(FacingMode.right);
                        Pushed();
                        break;
                    case CollisionMode.Top when corner == CornerMode.Right:
                        ChangeFacing(FacingMode.left);
                        Pushed();
                        break;
                }
            }
        }
        private void UpdateSprite()
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
            ShowSprite(SpriteFactory.Instance.CreateBeetleSprite(state.GetType().Name + facingString));
        }

        private void ChangeFacing(FacingMode facing)
        {
            Facing = facing;

            // Notice: The effect should be the same as changing state
            UpdateSprite();
        }
    }
}

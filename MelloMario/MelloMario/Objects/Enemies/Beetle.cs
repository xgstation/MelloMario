namespace MelloMario.Objects.Enemies
{
    #region

    using System;
    using MelloMario.Factories;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Blocks.BrickStates;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Characters.ProtectionStates;
    using MelloMario.Objects.Enemies.BeetleStates;
    using MelloMario.Objects.Items;
    using MelloMario.Objects.Miscs;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    [Serializable]
    internal class Beetle : BasePhysicalObject
    {
        private IBeetleState state;

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

        protected override void OnUpdate(int time)
        {
            state.Update(time);
        }

        protected override void OnSimulation(int time)
        {
            ApplyGravity();

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
            switch (target)
            {
                case Mario mario:
                    if (mode == CollisionMode.Top && corner == CornerMode.Top && !(mario.ProtectionState is Dead) || mario.ProtectionState is Starred)
                    {
                        Defeat();
                    }
                    else if ((state is KoopaStates.Normal || state is MovingShell) && !(mario.ProtectionState is Dead))
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
                        if (mode == CollisionMode.Left)
                        {
                            ChangeFacing(FacingMode.right);
                            Pushed();
                        }
                        else if (mode == CollisionMode.Right)
                        {
                            ChangeFacing(FacingMode.left);
                            Pushed();
                        }
                        else if (mode == CollisionMode.Top && corner == CornerMode.Left)
                        {
                            ChangeFacing(FacingMode.right);
                            Pushed();
                        }
                        else if (mode == CollisionMode.Top && corner == CornerMode.Right)
                        {
                            ChangeFacing(FacingMode.left);
                            Pushed();
                        }
                    }
                    break;
                case Brick brick when brick.State is Hidden:
                    break;
                case Question question when question.State is Blocks.QuestionStates.Hidden:
                    break;
                case IGameObject obj when target is Brick
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
                case FireBall fire:
                    Defeat();
                    break;
            }
            if (target is Koopa koopa)
            {
                if (koopa.State is KoopaStates.MovingShell || koopa.State is KoopaStates.NewlyMovingShell)
                {
                    Defeat();
                }
            }
            if (target is Beetle beetle)
            {
                if (beetle.State is MovingShell || beetle.State is NewlyMovingShell)
                {
                    Defeat();
                }
            }
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
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

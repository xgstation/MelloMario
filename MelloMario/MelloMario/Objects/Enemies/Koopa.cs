namespace MelloMario.Objects.Enemies
{
    #region

    using System;
    using MelloMario.Factories;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Blocks.BrickStates;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Characters.ProtectionStates;
    using MelloMario.Objects.Enemies.KoopaStates;
    using MelloMario.Objects.Items;
    using MelloMario.Objects.Miscs;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Koopa : BasePhysicalObject
    {
        private readonly string color;
        private IKoopaState state;

        public Koopa(IWorld world, Point location, IListener<IGameObject> listener, string color) :
            base(world, location, listener, new Point(32, 32), 32)
        {
            this.color = color;
            state = new KoopaStates.Normal(this);
            UpdateSprite();
        }

        public IKoopaState State
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
            ScorePoints(Const.SCORE_KOOPA);
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
            ScorePoints(Const.SCORE_KOOPA);
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
                    SetHorizontalVelocity(-Const.VELOCITY_KOOPA_SHELL);
                }
                else
                {
                    SetHorizontalVelocity(Const.VELOCITY_KOOPA_SHELL);
                }
            }
            else if (state is KoopaStates.Normal)
            {
                if (Facing == FacingMode.left)
                {
                    SetHorizontalVelocity(-Const.VELOCITY_KOOPA);
                }
                else
                {
                    SetHorizontalVelocity(Const.VELOCITY_KOOPA);
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
                    if (koopa.State is MovingShell || koopa.State is NewlyMovingShell)
                    {
                        Defeat();
                    }
                    break;
                case Beetle beetle:
                    if (beetle.State is BeetleStates.MovingShell || beetle.State is BeetleStates.NewlyMovingShell)
                    {
                        Defeat();
                    }
                    break;
            }
        }

        private void OnCollisionMario(Mario mario, CollisionMode mode, CornerMode corner)
        {
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
                    case CollisionMode.Bottom:
                        break;
                    case CollisionMode.InnerLeft:
                        break;
                    case CollisionMode.InnerRight:
                        break;
                    case CollisionMode.InnerTop:
                        break;
                    case CollisionMode.InnerBottom:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
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
            ShowSprite(SpriteFactory.Instance.CreateKoopaSprite(color, state.GetType().Name + facingString));
        }

        private void ChangeFacing(FacingMode facing)
        {
            Facing = facing;

            // Notice: The effect should be the same as changing state
            UpdateSprite();
        }
    }
}

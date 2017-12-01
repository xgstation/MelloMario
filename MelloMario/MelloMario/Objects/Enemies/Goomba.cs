namespace MelloMario.Objects.Enemies
{
    #region

    using System;
    using System.Diagnostics.CodeAnalysis;
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
    using MelloMario.Sounds.Effects;

    #endregion

    internal class Goomba : BasePhysicalObject
    {
        private IGoombaState state;

        public ISoundArgs SoundEventArgs { get; }

        public Goomba(IWorld world, Point location, IListener<IGameObject> listener) : base(
            world,
            location,
            listener,
            new Point(32, 32),
            32)
        {
            SoundEventArgs = new SoundArgs();
            state = new GoombaStates.Normal(this);
            UpdateSprite();
        }

        public IGoombaState State
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

        public void Defeat()
        {
            SoundEventArgs.SetMethodCalled();
            ScorePoints(Const.SCORE_GOOMBA);
            World.Add(new PopingUpPoints(World, Boundary.Location, Const.SCORE_GOOMBA));
            State.Defeat();
        }

        protected override void OnUpdate(int time)
        {
            state.Update(time);
        }

        protected override void OnSimulation(int time)
        {
            ApplyGravity();

            if (Facing == FacingMode.left)
            {
                SetHorizontalVelocity(-Const.VELOCITY_GOOMBA);
            }
            else
            {
                SetHorizontalVelocity(Const.VELOCITY_GOOMBA);
            }

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
            if (state is GoombaStates.Defeated)
            {
                return;
            }
            switch (target)
            {
                case Mario mario:
                    if (mode == CollisionMode.Top && !(mario.ProtectionState is Dead) || mario.ProtectionState is Starred)
                    {
                        Defeat();
                    }
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
                case Koopa koopa:
                    if (koopa.State is MovingShell || koopa.State is NewlyMovingShell)
                    {
                        Defeat();
                    }
                    break;
                case FireBall _:
                    Defeat();
                    break;
            }
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateGoombaSprite(state.GetType().Name));
        }

        private void ChangeFacing(FacingMode facing)
        {
            Facing = facing;

            // Notice: The effect should be the same as changing state
            UpdateSprite();
        }
    }
}

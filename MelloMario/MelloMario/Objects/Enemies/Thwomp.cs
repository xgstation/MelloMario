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
    using MelloMario.Objects.UserInterfaces;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    [Serializable]
    internal class Thwomp : BasePhysicalObject
    {
        private IThwompState state;

        public Thwomp(IWorld world, Point location, IListener<IGameObject> listener) : base(
            world,
            location,
            listener,
            new Point(32, 32),
            32)
        {
            state = new ThwompStates.Normal(this);
            UpdateSprite();
        }

        public IThwompState State
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

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateThwompSprite(state.GetType().Name));
        }

        protected override void OnUpdate(int time)
        {
            state.Update(time);
        }

        protected override void OnSimulation(int time)
        {
            ApplyGravity();

            if (Facing == FacingMode.left) // Make condition if he is colliding with floor tile or not
            {
                SetVerticalVelocity(-Const.VELOCITY_RISING_THWOMP);
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
            switch (target.GetType().Name)
            {
                case "MarioCharacter":
                    //TODO: Fire to be added
                    Mario mario = (Mario) target;
                    if (mario.ProtectionState is Starred)
                    {
                        Defeat();
                    }
                    break;
                case "Brick":
                    if (((Brick) target).State is Hidden)
                    {
                        break;
                    }
                    goto case "Stair";
                case "Question":
                    if (((Question) target).State is Blocks.QuestionStates.Hidden)
                    {
                        break;
                    }
                    goto case "Stair";
                case "Floor":
                // perhaps register that it is on the floor, wait a second, then rise back up?
                case "Pipeline":
                case "Stair":
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
                case "Koopa":
                    if (target is Koopa koopa)
                    {
                        if (koopa.State is MovingShell)
                        {
                            // Do nothing
                        }
                    }
                    break;
                case "Fire":
                    break;
            }
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
        }

        private void ChangeFacing(FacingMode facing)
        {
            Facing = facing;

            // Notice: The effect should be the same as changing state
            UpdateSprite();
        }

        public void Defeat()
        {
            ScorePoints(Const.SCORE_THWOMP);
            new PopingUpPoints(World, Boundary.Location, Const.SCORE_THWOMP);
            State.Defeat();
        }
    }
}

namespace MelloMario.Objects.Items
{
    #region

    using System;
    using MelloMario.Factories;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Blocks.BrickStates;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class FireBall : BasePhysicalObject
    {
        private int elapsed;

        public FireBall(IWorld world, Point location, IListener<IGameObject> listener, IListener<ISoundable> soundListener, bool facingRight) : base(
            world,
            location,
            listener,
            new Point(16, 16),
            32)
        {
            elapsed = 0;

            if (facingRight)
            {
                Facing = FacingMode.right;
            }
            else
            {
                Facing = FacingMode.left;
            }

            ShowSprite(SpriteFactory.Instance.CreateFireSprite());
        }

        protected override void OnSimulation(int time)
        {
            ApplyGravity();

            if (Facing == FacingMode.left)
            {
                SetHorizontalVelocity(-Const.VELOCITY_FIRE_H);
            }
            else
            {
                SetHorizontalVelocity(Const.VELOCITY_FIRE_H);
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
                case Brick brick when brick.State is Hidden:
                    break;
                case Question question when question.State is Blocks.QuestionStates.Hidden:
                    break;
                case IGameObject _ when target is Brick
                || target is Question
                || target is Floor
                || target is Pipeline
                || target is Stair:
                    if (mode == CollisionMode.Top)
                    {
                        Bounce(mode, new Vector2());
                    }
                    if (mode == CollisionMode.Left
                        || mode == CollisionMode.InnerLeft && corner == CornerMode.Center)
                    {
                        Bounce(mode, new Vector2(), 1);
                        Facing = FacingMode.right;
                    }
                    else if (mode == CollisionMode.Right
                        || mode == CollisionMode.InnerRight && corner == CornerMode.Center)
                    {
                        Bounce(mode, new Vector2(), 1);
                        Facing = FacingMode.left;
                    }
                    if (mode == CollisionMode.Bottom
                        || mode == CollisionMode.InnerBottom && corner == CornerMode.Center)
                    {
                        Bounce(mode, new Vector2());
                        SetVerticalVelocity(-Const.VELOCITY_FIRE_V);
                    }
                    break;
            }
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnUpdate(int time)
        {
            elapsed += time;

            if (elapsed > 2500)
            {
                RemoveSelf();
            }
        }
    }
}

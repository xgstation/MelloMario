namespace MelloMario.Objects.Items
{
    #region

    using System;
    using MelloMario.Factories;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Blocks.BrickStates;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Items.SuperMushroomStates;
    using MelloMario.Objects.UserInterfaces;
    using MelloMario.Sounds;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    [Serializable]
    internal class SuperMushroom : BasePhysicalObject, ISoundable
    {
        private bool collected;
        private IItemState state;
        private readonly IListener<IGameObject> listener;
        private readonly IListener<ISoundable> soundListener;

        public SuperMushroom(
            IWorld world,
            Point location,
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener,
            bool isUnveil = false) : base(world, location, listener, new Point(32, 32), 32)
        {
            collected = false;
            this.listener = listener;
            this.soundListener = soundListener;
            soundListener.Subscribe(this);
            SoundEventArgs = new SoundArgs();
            SoundEventArgs.SetMethodCalled();
            if (isUnveil)
            {
                state = new Unveil(this);
                UpdateSprite();
                RemoveSelf();
            }
            else
            {
                state = new SuperMushroomStates.Normal(this);
                UpdateSprite();
            }
        }

        public IItemState State
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

        public IGameObject GetFireFlower()
        {
            return GameObjectFactory.Instance.CreateGameObject(
                "FireFlowerUnveil",
                World,
                Boundary.Location,
                listener,
                soundListener);
        }

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateSuperMushroomSprite());
        }

        protected override void OnUpdate(int time)
        {
            SoundEvent?.Invoke(this, SoundEventArgs);
            state.Update(time);
        }

        protected override void OnSimulation(int time)
        {
            if (state is SuperMushroomStates.Normal)
            {
                ApplyGravity();

                if (Facing == FacingMode.left)
                {
                    SetHorizontalVelocity(-Const.VELOCITY_SUPER_MUSHROOM);
                }
                else
                {
                    SetHorizontalVelocity(Const.VELOCITY_SUPER_MUSHROOM);
                }
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
                    if (state is OneUpMushroomStates.Normal)
                    {
                        Collect();
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
                    if (mode == CollisionMode.Top || mode == CollisionMode.Bottom)
                    {
                        Bounce(mode, new Vector2());
                    }
                    if (mode == CollisionMode.Left || mode == CollisionMode.InnerLeft && corner == CornerMode.Center)
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
                    break;
            }
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        public void Collect()
        {
            if (!collected)
            {
                ScorePoints(Const.SCORE_POWER_UP);
                new PopingUpPoints(World, Boundary.Location, Const.SCORE_POWER_UP);
            }
            collected = true;
            RemoveSelf();
            //State.Collect();
        }

        public void UnveilMove(int delta)
        {
            Move(new Point(0, delta));
        }

        public event SoundHandler SoundEvent;
        public ISoundArgs SoundEventArgs { get; }
    }
}

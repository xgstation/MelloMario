namespace MelloMario.Objects.Blocks
{
    #region

    using System;
    using MelloMario.Factories;
    using MelloMario.Objects.Blocks.BrickStates;
    using MelloMario.Objects.Characters;
    using MelloMario.Sounds;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    [Serializable]
    internal class Brick : BaseCollidableObject, ISoundable
    {
        private bool isHidden;
        private IBlockState state;
        public ISoundArgs SoundEventArgs { get; }

        public Brick(IWorld world, Point location, IListener<IGameObject> listener, IListener<ISoundable> soundListener, bool isHidden = false) : base(
            world,
            location,
            listener,
            new Point(32, 32))
        {
            this.isHidden = isHidden;
            soundListener.Subscribe(this);
            SoundEventArgs = new SoundArgs();
        }

        public bool HasInitialItem { get; private set; }

        public IBlockState State
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

        public void Initialize(bool hidden = false)
        {
            isHidden = hidden;
            if (isHidden)
            {
                state = new Hidden(this);
            }
            else
            {
                state = new Normal(this);
            }
            HasInitialItem = Database.HasItemEnclosed(this);
            UpdateSprite();
        }

        private void UpdateSprite()
        {
            switch (state)
            {
                case IState s when s is Hidden:
                    HideSprite();
                    break;
                case IState s when s is Destroyed:
                    ShowSprite(SpriteFactory.Instance.CreateBrickSprite("Destroyed"));
                    break;
                case IState s when s is Normal:
                    ShowSprite(SpriteFactory.Instance.CreateBrickSprite("Normal"));
                    break;
                case IState s when s is Used:
                    ShowSprite(SpriteFactory.Instance.CreateQuestionSprite("Used"));
                    break;
            }
        }

        protected override void OnUpdate(int time)
        {
            state.Update(time);
        }

        protected override void OnSimulation(int time)
        {
            SoundEvent?.Invoke(this, SoundEventArgs);
            base.OnSimulation(time);
        }

        protected override void OnCollision(
            IGameObject target,
            CollisionMode mode,
            CollisionMode modePassive,
            CornerMode corner,
            CornerMode cornerPassive)
        {
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
        }

        public void OnDestroy()
        {
            SoundEventArgs.SetMethodCalled();
            ScorePoints(Const.SCORE_BRICK);
        }

        public void Remove()
        {
            RemoveSelf();
        }

        public void Bump(Mario mario)
        {
            SoundEventArgs.SetMethodCalled();
            State.Bump(mario);
        }

        public void BumpMove(int delta)
        {
            Move(new Point(0, delta));
        }

        public void ReleaseNextItem()
        {
            if (!Database.HasItemEnclosed(this))
            {
                return;
            }
            IGameObject item = Database.GetNextItem(this);
            World.Update();
            World.Add(item);
        }

        public event SoundHandler SoundEvent;
    }
}

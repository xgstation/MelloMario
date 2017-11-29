namespace MelloMario.Objects.Blocks
{
    #region

    using System;
    using MelloMario.Factories;
    using MelloMario.Objects.Blocks.QuestionStates;
    using MelloMario.Objects.Characters;
    using MelloMario.Sounds.Effects;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    [Serializable]
    internal class Question : BaseCollidableObject, ISoundable
    {
        private bool isHidden;
        private IBlockState state;

        public Question(IWorld world, Point location, IListener<IGameObject> listener, IListener<ISoundable> soundListener, bool isHidden = false) :
            base(world, location, listener, new Point(32, 32))
        {
            this.isHidden = isHidden;
            soundListener.Subscribe(this);
            SoundEventArgs = new SoundArgs();
        }

        public ISoundArgs SoundEventArgs { get; }

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

        public event SoundHandler SoundEvent;

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
            UpdateSprite();
        }

        public void Bump(Mario mario)
        {
            State.Bump(mario);
            SoundEventArgs.SetMethodCalled();
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

        protected override void OnUpdate(int time)
        {
            state.Update(time);
        }

        protected override void OnSimulation(int time)
        {
            base.OnSimulation(time);
            SoundEvent?.Invoke(this, SoundEventArgs);
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

        private void UpdateSprite()
        {
            switch (state)
            {
                case IState s when s is Hidden:
                    HideSprite();
                    break;
                case IState s when s is Normal:
                    ShowSprite(SpriteFactory.Instance.CreateQuestionSprite("Normal"));
                    break;
                case IState s when s is Used:
                    ShowSprite(SpriteFactory.Instance.CreateQuestionSprite("Used"));
                    break;
            }
        }
    }
}

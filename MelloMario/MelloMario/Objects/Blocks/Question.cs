namespace MelloMario.Objects.Blocks
{
    #region

    using MelloMario.Factories;
    using MelloMario.Objects.Blocks.QuestionStates;
    using MelloMario.Objects.Characters;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class Question : BaseCollidableObject
    {
        private bool isHidden;
        private IBlockState state;

        public Question(IGameWorld world, Point location, IListener listener, bool isHidden = false) : base(
            world,
            location,
            listener,
            new Point(32, 32))
        {
            this.isHidden = isHidden;
        }

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
            UpdateSprite();
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

        protected override void OnUpdate(int time)
        {
            state.Update(time);
        }

        protected override void OnCollision(
            IGameObject target,
            CollisionMode mode,
            CollisionMode modePassive,
            CornerMode corner,
            CornerMode cornerPassive) { }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode, CollisionMode modePassive) { }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive) { }

        protected override void OnDraw(int time, SpriteBatch spriteBatch) { }

        public void Bump(Mario mario)
        {
            //TODO:Move this into soundcontroller
            //SoundController.BumpBlock.Play();
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
    }
}

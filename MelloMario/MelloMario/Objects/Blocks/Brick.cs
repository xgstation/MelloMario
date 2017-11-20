namespace MelloMario.Objects.Blocks
{
    #region

    using BrickStates;
    using Characters;
    using Factories;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Theming;

    #endregion

    internal class Brick : BaseCollidableObject
    {
        private bool isHidden;
        private IBlockState state;

        public Brick(IGameWorld world, Point location, IListener listener) : this(world, location, listener, false) { }

        public Brick(IGameWorld world, Point location, IListener listener, bool isHidden = false) : base(world, location, listener, new Point(32, 32))
        {
            this.isHidden = isHidden;
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

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionMode modePassive, CornerMode corner, CornerMode cornerPassive) { }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode, CollisionMode modePassive) { }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive) { }

        protected override void OnDraw(int time, SpriteBatch spriteBatch) { }

        public void OnDestoy()
        {
            ScorePoints(Const.SCORE_BRICK);
        }

        public void Remove()
        {
            RemoveSelf();
        }

        public void Bump(Mario mario)
        {
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

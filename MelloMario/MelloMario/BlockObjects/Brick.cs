using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.BrickStates;
using MelloMario.MarioObjects;
using MelloMario.Theming;

namespace MelloMario.BlockObjects
{
    class Brick : BaseCollidableObject
    {
        private IBlockState state;
        private bool isHidden;
        private bool hasInitialItem;

        public bool HasInitialItem
        {
            get
            {
                return hasInitialItem;
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
            hasInitialItem = GameDatabase.HasItemEnclosed(this);
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

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionMode modePassive, CornerMode corner, CornerMode cornerPassive)
        {
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnDraw(int time, Rectangle viewport)
        {
        }

        public void OnDestoy()
        {
            ScorePoints(GameConst.SCORE_BRICK);
        }

        public void Remove()
        {
            RemoveSelf();
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

        public Brick(IGameWorld world, Point location, Listener listener) : this(world, location, listener, false)
        {
        }

        public Brick(IGameWorld world, Point location, Listener listener, bool isHidden = false) : base(world, location, listener, new Point(32, 32))
        {
            this.isHidden = isHidden;
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
            if (!GameDatabase.HasItemEnclosed(this))
            {
                return;
            }
            IGameObject item = GameDatabase.GetNextItem(this);
            world.Update();
            world.Add(item);
        }
    }
}

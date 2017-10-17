using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.BrickStates;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects
{
    class Brick : BaseGameObject
    {
        private IBlockState state;

        private void OnStateChanged()
        {
            if (state is Hidden)
            {
                HideSprite();
            }
            else
            {
                ShowSprite(SpriteFactory.Instance.CreateBrickSprite(state.GetType().Name));
            }
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode)
        {
        }

        protected override void OnOut(CollisionMode mode)
        {
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
                OnStateChanged();
            }
        }

        public Brick(IGameWorld world, Point location) : base(world, location, new Point(32, 32))
        {
            state = new Normal(this);
            OnStateChanged();
        }
        public Brick(IGameWorld world, Point location, bool isHidden) : base(world, location, new Point(32, 32))
        {
            if (isHidden)
            {
                state = new Hidden(this);
            }
            else
            {
                state = new Normal(this);
            }
            OnStateChanged();
        }
        public void Show()
        {
            State.Show();
        }
        public void Hide()
        {
            State.Hide();
        }
        public void Bump(Mario mario)
        {
            State.Bump(mario);
        }
    }
}

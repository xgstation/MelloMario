using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.CoinStates;
using MelloMario.MarioObjects;

namespace MelloMario.ItemObjects
{
    class Coin : BaseGameObject
    {
        private IItemState state;

        private void OnStateChanged()
        {
            ShowSprite(SpriteFactory.Instance.CreateCoinSprite());
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode)
        {
            if (target is Mario && !(state is Unveil))
            {
                Collect();
            }
        }

        protected override void OnOut(CollisionMode mode)
        {
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
                OnStateChanged();
            }
        }

        public Coin(IGameWorld world, Point location, bool isUnveil) : base(world, location, new Point(32, 32))
        {
            if (isUnveil)
            {
                state = new Unveil(this);
            } else
            {
                state = new Normal(this);
            }
            OnStateChanged();
        }
        public Coin(IGameWorld world, Point location) : this(world, location, false)
        {

        }

        public void Show()
        {
            State.Show();
        }
        public void Collect()
        {
            State.Collect();
        }
    }
}

using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.CoinStates;

namespace MelloMario.ItemObjects
{
    class Coin : BaseGameObject
    {
        private IItemState state;

        private void OnStateChanged()
        {
            // TODO: load different sprites base on the state
            ISprite sprite = SpriteFactory.Instance.CreateCoinSprite();
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target)
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

        public Coin(Point location) : base(location, new Point(32, 32))
        {
            state = new CoinNormal(this);
            OnStateChanged();
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

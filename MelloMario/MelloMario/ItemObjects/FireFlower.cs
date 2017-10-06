using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.FireFlowerStates;

namespace MelloMario.ItemObjects
{
    class FireFlower : BaseGameObject
    {
        private IItemState state;

        private void OnStateChanged()
        {
            if (state is FireFlowerNormal)
            {
                ShowSprite(SpriteFactory.Instance.CreateFireFlowerSprite());
            }
            else if (state is FireFlowerDefeated)
            {
                HideSprite();
            }
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

        public FireFlower(Point location) : base(location, new Point(32, 32))
        {
            state = new FireFlowerNormal(this);
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

using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.OneUpMushroomStates;

namespace MelloMario.ItemObjects
{
    class OneUpMushroom : BaseGameObject
    {
        private IItemState state;

        private void OnStateChanged()
        {
            if (state is OneUpMushroomNormal)
            {
                ShowSprite(SpriteFactory.Instance.CreateOneUpMushroomSprite());
            }
            else if (state is OneUpMushroomDefeated)
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

        public OneUpMushroom(Point location) : base(location, new Point(32, 32))
        {
            state = new OneUpMushroomNormal(this);
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

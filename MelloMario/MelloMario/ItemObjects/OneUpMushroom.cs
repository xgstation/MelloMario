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
            ShowSprite(SpriteFactory.Instance.CreateOneUpMushroomSprite());
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

        public OneUpMushroom(IGameWorld world, Point location) : base(world, location, new Point(32, 32))
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

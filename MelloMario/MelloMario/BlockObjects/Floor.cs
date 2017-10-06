using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.FloorStates;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects
{
    class Floor : BaseGameObject
    {
        private IBlockState state;

        private void OnStateChanged()
        {
            if (state is FloorHidden)
            {
                HideSprite();
            }
            else if (state is FloorNormal)
            {
                ShowSprite(SpriteFactory.Instance.CreateFloorSprite());
            }
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode)
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

        public Floor(Point location) : base(location, new Point(32, 32))
        {
            state = new FloorNormal(this);
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

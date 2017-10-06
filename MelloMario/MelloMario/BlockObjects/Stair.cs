using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.StairStates;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects
{
    class Stair : BaseGameObject
    {
        private IBlockState state;

        private void OnStateChanged()
        {
            if (state is StairHidden)
            {
                HideSprite();
            }
            else if (state is StairNormal)
            {
                ShowSprite(SpriteFactory.Instance.CreateStairSprite());
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

        public Stair(Point location) : base(location, new Point(32, 32))
        {
            state = new StairNormal(this);
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

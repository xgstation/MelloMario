using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.BrickStates;

namespace MelloMario.BlockObjects
{
    class Brick : BaseGameObject
    {
        private IBlockState state;

        private void OnStateChanged()
        {
            if (state is BrickBumped)
            {
                ShowSprite(SpriteFactory.Instance.CreateBrickSprite("Normal"));
            }
            if (state is BrickDestroyed)
            {
                // TODO: design a new mechanism to handle multiple sprites (class SpriteGroup : ISprite)?
                // ShowSprite(SpriteFactory.Instance.CreateBrickSprite("Destroyed"));
            }
            if (state is BrickHidden)
            {
                HideSprite();
            }
            if (state is BrickNormal)
            {
                ShowSprite(SpriteFactory.Instance.CreateBrickSprite("Normal"));
            }
            if (state is BrickUsed)
            {
                ShowSprite(SpriteFactory.Instance.CreateBrickSprite("Used"));
            }
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target)
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

        public Brick(Point location) : base(location, new Point(32, 32))
        {
            state = new BrickNormal(this);
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
        public void Bump()
        {
            State.Bump();
        }
        public void Destroy()
        {
            State.Destroy();
        }
        public void UseUp()
        {
            State.UseUp();
        }
    }
}

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
            if (state is BrickBumped)
            {
                ShowSprite(SpriteFactory.Instance.CreateBrickSprite("Normal"));
            }
            else if (state is BrickDestroyed)
            {
                // TODO: design a new mechanism to handle multiple sprites (class SpriteGroup : ISprite)?
                // ShowSprite(SpriteFactory.Instance.CreateBrickSprite("Destroyed"));
            }
            else if (state is BrickHidden)
            {
                HideSprite();
            }
            else if (state is BrickNormal)
            {
                ShowSprite(SpriteFactory.Instance.CreateBrickSprite("Normal"));
            }
            else if (state is BrickUsed)
            {
                ShowSprite(SpriteFactory.Instance.CreateBrickSprite("Used"));
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
        public void Bump(Mario mario)
        {
            State.Bump(mario);
        }
    }
}

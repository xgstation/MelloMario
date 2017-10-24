using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.StairStates;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects
{
    class Stair : BaseCollidableObject
    {
        private IBlockState state;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateStairSprite());
        }

        protected override void OnUpdate(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
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
                UpdateSprite();
            }
        }

        public Stair(IGameWorld world, Point location) : base(world, location, new Point(32, 32))
        {
            state = new Normal(this);
            UpdateSprite();
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

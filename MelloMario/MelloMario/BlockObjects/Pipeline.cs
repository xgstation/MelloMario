using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.PipelineStates;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects
{
    class Pipeline : BaseGameObject
    {
        private IBlockState state;
        private string type;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreatePipelineSprite(type));
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, ZIndex zIndex)
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

        public Pipeline(IGameWorld world, Point location, string type) : base(world, location, new Point(32, 32))
        {
            state = new Normal(this);
            this.type = type;
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

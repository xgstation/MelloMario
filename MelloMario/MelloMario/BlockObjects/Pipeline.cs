using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.PipelineStates;

namespace MelloMario.BlockObjects
{
    class Pipeline : BaseGameObject
    {
        private IBlockState state;

        private void OnStateChanged()
        {
            if (state is PipelineNormal)
            {
                ShowSprite(SpriteFactory.Instance.CreatePipelineSprite());
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

        public Pipeline(Point location) : base(location, new Point(32, 32))
        {
            state = new PipelineNormal(this);
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

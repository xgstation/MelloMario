using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.QuestionStates;

namespace MelloMario.BlockObjects
{
    class Question : BaseGameObject
    {
        private IBlockState state;

        private void OnStateChanged()
        {
            // if () ...
            // ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireIdleRight", false), ResizeModeX.Center, ResizeModeY.Bottom);
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

        public Question(Point location) : base(location, new Point(32, 32))
        {
            state = new QuestionNormal(this);
            //I am not sure where you wanted the sprite attached, this seems like a logical place
            ISprite sprite = SpriteFactory.Instance.CreateQuestionSprite("Normal");
            ShowSprite(sprite);
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

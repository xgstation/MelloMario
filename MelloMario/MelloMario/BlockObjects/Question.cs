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
            if (state is QuestionBumped)
            {
                ShowSprite(SpriteFactory.Instance.CreateQuestionSprite("Normal"));
            }
            else if (state is QuestionHidden)
            {
                HideSprite();
            }
            else if (state is QuestionNormal)
            {
                ShowSprite(SpriteFactory.Instance.CreateQuestionSprite("Normal"));
            }
            else if (state is QuestionUsed)
            {
                ShowSprite(SpriteFactory.Instance.CreateQuestionSprite("Used"));
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

        public Question(Point location) : base(location, new Point(32, 32))
        {
            state = new QuestionNormal(this);
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

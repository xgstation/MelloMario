using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.EnemyObjects.KoopaStates;

namespace MelloMario.EnemyObjects
{
    class Koopa : BaseGameObject
    {
        private ShellColor color;
        private IKoopaState state;

        private void OnStateChanged()
        {
            switch (color)
            {
                case ShellColor.green:
                    ShowSprite(SpriteFactory.Instance.CreateGreenKoopaSprite(state.GetType().Name));
                    break;
                case ShellColor.red:
                    ShowSprite(SpriteFactory.Instance.CreateRedKoopaSprite(state.GetType().Name));
                    break;
            }
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode)
        {
        }

        public enum ShellColor { green, red };

        public IKoopaState State
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

        public Koopa(Point location, ShellColor color) : base(location, new Point(32, 32))
        {
            this.color = color;

            state = new Normal(this);
            OnStateChanged();
        }

        public void Show()
        {
            State.Show();
        }
        public void JumpOn()
        {
            State.JumpOn();
        }
        public void Defeat()
        {
            State.Defeat();
        }
    }
}

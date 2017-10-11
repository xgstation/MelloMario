using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.EnemyObjects.GoombaStates;

namespace MelloMario.EnemyObjects
{
    class Goomba : BaseGameObject
    {
        private IGoombaState state;

        private void OnStateChanged()
        {
            ShowSprite(SpriteFactory.Instance.CreateGoombaSprite(state.GetType().Name));
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode)
        {
        }

        public IGoombaState State
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

        public Goomba(Point location) : base(location, new Point(32, 32))
        {
            state = new Normal(this);
            OnStateChanged();
        }

        public void Show()
        {
            State.Show();
        }
        public void Defeat()
        {
            State.Defeat();
        }
    }
}

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
            if (state is GoombaNormal)
            {
                ShowSprite(SpriteFactory.Instance.CreateGoombaSprite("Normal"));
            }
            else if (state is GoombaDefeated)
            {
                ShowSprite(SpriteFactory.Instance.CreateGoombaSprite("Defeated"));
            }
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
            state = new GoombaNormal(this);
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

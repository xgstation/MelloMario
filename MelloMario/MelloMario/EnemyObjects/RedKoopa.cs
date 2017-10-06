using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.EnemyObjects.KoopaStates;

namespace MelloMario.EnemyObjects
{
    class RedKoopa : BaseGameObject
    {
        private IKoopaState state;

        private void OnStateChanged()
        {
            if (state is RedKoopaNormal)
            {
                ShowSprite(SpriteFactory.Instance.CreateRedKoopaSprite("Normal"));
            }
            else if (state is RedKoopaJumpOn)
            {
                ShowSprite(SpriteFactory.Instance.CreateRedKoopaSprite("JumpOn"));
            }
            else if (state is RedKoopaDefeated)
            {
                ShowSprite(SpriteFactory.Instance.CreateRedKoopaSprite("Defeated"));
            }
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode)
        {
        }

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

        public RedKoopa(Point location) : base(location, new Point(32, 32))
        {
            state = new RedKoopaNormal(this);
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

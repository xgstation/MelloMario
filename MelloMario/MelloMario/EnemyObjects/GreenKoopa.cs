using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.EnemyObjects.KoopaStates;

namespace MelloMario.EnemyObjects
{
    class GreenKoopa : BaseGameObject
    {
        private IKoopaState state;

        private void OnStateChanged()
        {
            if (state is GreenKoopaNormal)
            {
                ShowSprite(SpriteFactory.Instance.CreateGreenKoopaSprite("Normal"));
            }
            else if (state is GreenKoopaJumpOn)
            {
                ShowSprite(SpriteFactory.Instance.CreateGreenKoopaSprite("JumpOn"));
            }
            else if (state is GreenKoopaDefeated)
            {
                ShowSprite(SpriteFactory.Instance.CreateGreenKoopaSprite("Defeated"));
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

        public GreenKoopa(Point location) : base(location, new Point(32, 32))
        {
            state = new GreenKoopaNormal(this);
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

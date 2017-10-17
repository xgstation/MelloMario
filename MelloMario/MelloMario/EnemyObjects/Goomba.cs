using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects;
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
            if (target is Mario)
            {
                //TODO: Fire to be added
                Mario m = (Mario)target;
                if(mode == CollisionMode.Top || m.ProtectionState is MarioObjects.ProtectionStates.Protected)
                {
                    Defeat();
                }
            }
            else if (target is Koopa)
            {
                Koopa k = (Koopa)target;
                if (k.State is KoopaStates.MovingShell)
                {
                    Defeat();
                }
            }
            
        }

        protected override void OnOut(CollisionMode mode)
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

        public Goomba(IGameWorld world, Point location) : base(world, location, new Point(32, 32))
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

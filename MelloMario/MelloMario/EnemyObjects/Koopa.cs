using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects;
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
            if (target is Mario)
            {
                //TODO: fire as target to be added
                Mario m = (Mario)target;
                if (m.ProtectionState is MarioObjects.ProtectionStates.Starred)
                {
                    Defeat();
                }
                else
                {
                    if (state is Normal)
                    {
                        if (mode == CollisionMode.Top)
                            JumpOn();
                    }
                    else if (state is Shell)
                    {
                        if (mode == CollisionMode.Left || mode == CollisionMode.Top)
                        {
                            Pushed();
                        }
                        else
                        {
                            Pushed();
                        }
                      
                    }
                }
            }
        }

        protected override void OnOut(CollisionMode mode)
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

        public Koopa(IGameWorld world, Point location, ShellColor color) : base(world, location, new Point(32, 32))
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
        public void Pushed()
        {
            State.Pushed();
        }
        public void Defeat()
        {
            State.Defeat();
        }
    }
}

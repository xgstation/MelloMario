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
                    if (state is KoopaNormal)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateGreenKoopaSprite("Normal"));
                    }
                    else if (state is KoopaJumpOn)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateGreenKoopaSprite("JumpOn"));
                    }
                    else if (state is KoopaDefeated)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateGreenKoopaSprite("Defeated"));
                    }
                    break;
                case ShellColor.red:
                    if (state is KoopaNormal)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateRedKoopaSprite("Normal"));
                    }
                    else if (state is KoopaJumpOn)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateRedKoopaSprite("JumpOn"));
                    }
                    else if (state is KoopaDefeated)
                    {
                        ShowSprite(SpriteFactory.Instance.CreateRedKoopaSprite("Defeated"));
                    }
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

            state = new KoopaNormal(this);
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

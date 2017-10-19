using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects;
using MelloMario.EnemyObjects.KoopaStates;

namespace MelloMario.EnemyObjects
{
    class Koopa : BasePhysicalObject
    {
        private ShellColor color;
        private IKoopaState state;

        private void OnStateChanged()
        {
            string facingString;
            if (Facing == FacingMode.left)
            {
                facingString = "Left";
            }
            else
            {
                facingString = "Right";
            }
            switch (color)
            {
                case ShellColor.green:
                    ShowSprite(SpriteFactory.Instance.CreateGreenKoopaSprite(state.GetType().Name + facingString));
                    break;
                case ShellColor.red:
                    ShowSprite(SpriteFactory.Instance.CreateRedKoopaSprite(state.GetType().Name + facingString));
                    break;
            }
        }
        private void ChangeFacing(FacingMode facing)
        {
            Facing = facing;

            // Notice: The effect should be the same as changing Mario's state
            OnStateChanged();
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionCornerMode corner, CollisionCornerMode cornerPassive)
        {
            if (target is Mario mario)
            {
                //TODO: fire as target to be added
                if (mario.ProtectionState is MarioObjects.ProtectionStates.Starred)
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

        protected override void OnDraw(GameTime time)
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

        public Koopa(IGameWorld world, Point location, ShellColor color) : base(world, location, new Point(32, 32), 32)
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

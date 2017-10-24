using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects;
using MelloMario.EnemyObjects.GoombaStates;

namespace MelloMario.EnemyObjects
{
    class Goomba : BaseCollidableObject
    {
        private IGoombaState state;

        private void UpdateSprite()
        {

            ShowSprite(SpriteFactory.Instance.CreateGoombaSprite(state.GetType().Name));
        }

        protected override void OnSimulation(GameTime time)
        {

        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is Mario mario)
            {
                //TODO: Fire to be added
                if (mode == CollisionMode.Top || mario.ProtectionState is MarioObjects.ProtectionStates.Starred)
                {
                    Defeat();
                }
            }
            else if (target is Koopa koopa)
            {
                if (koopa.State is KoopaStates.MovingShell)
                {
                    Defeat();
                }
            }

        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
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
                UpdateSprite();
            }
        }

        public Goomba(IGameWorld world, Point location) : base(world, location, new Point(32, 32))
        {
            state = new Normal(this);
            UpdateSprite();
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

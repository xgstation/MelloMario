using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.SuperMushroomStates;
using MelloMario.MarioObjects;

namespace MelloMario.ItemObjects
{
    class SuperMushroom : BaseGameObject
    {
        private IItemState state;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateSuperMushroomSprite());
        }

        protected override void OnSimulation(GameTime time)
        {
            state.Update(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionCornerMode corner, CollisionCornerMode cornerPassive)
        {
            if (target is Mario && state is Normal)
            {
                Collect();
            }
        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        public IItemState State
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

        protected override void OnDraw(GameTime time, ZIndex zIndex)
        {
        }

        public SuperMushroom(IGameWorld world, Point location, bool isUnveil) : base(world, location, new Point(32, 32))
        {
            if (isUnveil)
            {
                state = new Unveil(this);
            }
            else
            {
                state = new Normal(this);
            }
            UpdateSprite();
        }
        public SuperMushroom(IGameWorld world, Point location) : this(world, location, false)
        {
        }

        public void Show()
        {
            State.Show();
        }
        public void Collect()
        {
            RemoveSelf();
            //State.Collect();
        }

        public void UnveilMove(int delta)
        {
            Move(new Point(0, delta));
        }
    }
}

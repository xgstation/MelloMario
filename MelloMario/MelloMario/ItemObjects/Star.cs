using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.StarStates;
using MelloMario.MarioObjects;

namespace MelloMario.ItemObjects
{
    class Star : BaseGameObject
    {
        private IItemState state;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateStarSprite());
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionCornerMode corner, CollisionCornerMode cornerPassive)
        {
            if (target is Mario)
            {
                Collect();
            }
        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, ZIndex zIndex)
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

        public Star(IGameWorld world, Point location, bool isUnveil) : base(world, location, new Point(32, 32))
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
        public Star(IGameWorld world, Point location) : this(world, location, false)
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
    }
}

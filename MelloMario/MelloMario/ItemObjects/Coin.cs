using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.CoinStates;
using MelloMario.MarioObjects;

namespace MelloMario.ItemObjects
{
    class Coin : BaseCollidableObject
    {
        private IItemState state;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateCoinSprite());
        }

        protected override void OnUpdate(GameTime time)
        {
            state.Update(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is Mario && state is Normal)
            {
                Collect();
            }
        }

        protected override void OnSeen(IPlayer player, CollisionMode mode)
        {
        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
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

        public Coin(IGameWorld world, Point location, bool isUnveil) : base(world, location, new Point(32, 32))
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

        public Coin(IGameWorld world, Point location) : this(world, location, false)
        {
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

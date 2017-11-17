using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.FireFlowerStates;
using MelloMario.MarioObjects;
using MelloMario.UIObjects;
using MelloMario.Theming;

namespace MelloMario.ItemObjects
{
    class FireFlower : BaseCollidableObject
    {
        private IItemState state;
        private bool collected;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateFireFlowerSprite());
        }

        protected override void OnUpdate(int time)
        {
            state.Update(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionMode modePassive, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is Mario && state is Normal)
            {
                Collect();
            }
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnDraw(int time, Rectangle viewport)
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

        public FireFlower(IGameWorld world, Point location, Listener listener, bool isUnveil) : base(world, location, listener, new Point(32, 32))
        {
            collected = false;
            if (isUnveil)
            {
                state = new Unveil(this);
                UpdateSprite();
                RemoveSelf();
            }
            else
            {
                state = new Normal(this);
                UpdateSprite();
            }
        }

        public FireFlower(IGameWorld world, Point location, Listener listener) : this(world, location, listener, false)
        {
        }

        public void Collect()
        {
            if (!collected)
            {
                ScorePoints(GameConst.SCORE_POWER_UP);
                new PopingUpPoints(World, Boundary.Location, GameConst.SCORE_POWER_UP);
            }
            collected = true;
            RemoveSelf();
            //State.Collect();
        }

        public void UnveilMove(int delta)
        {
            Move(new Point(0, delta));
        }
    }
}

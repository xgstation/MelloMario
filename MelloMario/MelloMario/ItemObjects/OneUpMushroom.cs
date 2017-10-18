using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.OneUpMushroomStates;
using MelloMario.MarioObjects;

namespace MelloMario.ItemObjects
{
    class OneUpMushroom : BaseGameObject
    {
        private IItemState state;

        private void OnStateChanged()
        {
            ShowSprite(SpriteFactory.Instance.CreateOneUpMushroomSprite());
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode)
        {
            if (target is Mario)
            {
                Collect();
            }
            //Collide with bumped brick to be done
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
                OnStateChanged();
            }
        }

        public OneUpMushroom(IGameWorld world, Point location, bool isUnveil) : base(world, location, new Point(32, 32))
        {
            if (isUnveil)
            {
                state = new Unveil(this);
            }
            else
            {
                state = new Normal(this);
            }
            OnStateChanged();
        }
        public OneUpMushroom(IGameWorld world, Point location) : this (world, location, false)
        {
        }

        public void Show()
        {
            State.Show();
        }
        public void Collect()
            RemoveSelf();
        //State.Collect();
    }
}
}

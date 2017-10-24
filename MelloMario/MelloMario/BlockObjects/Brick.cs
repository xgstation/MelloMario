using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.BrickStates;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects
{
    class Brick : BaseCollidableObject
    {
        private IBlockState state;
        private IEnumerator<IGameObject> items;
        private IGameObject item;

        private void UpdateSprite()
        {
            if (state is Hidden)
            {
                HideSprite();
            }
            else
            {
                ShowSprite(SpriteFactory.Instance.CreateBrickSprite(state.GetType().Name));
            }
        }

        private void LoadItem()
        {
            if (items.MoveNext())
            {
                item = items.Current;
            }
            else
            {
                item = null;
            }
        }

        protected override void OnUpdate(GameTime time)
        {
            state.Update(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
        }

        public void Remove()
        {
            RemoveSelf();
        }

        public IBlockState State
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

        public Brick(IGameWorld world, Point location, bool isHidden = false) : this(world, location, new List<IGameObject>(), isHidden)
        {
        }

        public Brick(IGameWorld world, Point location, IEnumerable<IGameObject> items, bool isHidden = false) : base(world, location, new Point(32, 32))
        {
            if (isHidden)
            {
                state = new Hidden(this);
            }
            else
            {
                state = new Normal(this);
            }
            UpdateSprite();

            this.items = items.GetEnumerator();
            LoadItem();
        }


        public void Bump(Mario mario)
        {
            State.Bump(mario);

        }

        public void BumpMove(int delta)
        {
            Move(new Point(0, delta));

        }

        public bool ReleaseNextItem()
        {
            if (item != null)
            {
                World.AddObject(item);
                LoadItem();
            }

            return item != null;
        }
    }
}

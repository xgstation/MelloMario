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
        private IList<IGameObject> items;
        private IGameObject item;
        private readonly bool hasInitialItem;
        private void UpdateSprite()
        {
            if (state is Hidden)
            {
                HideSprite();
            }
            else if (hasInitialItem)
            {
                if (HasItem)
                {
                    ShowSprite(SpriteFactory.Instance.CreateBrickSprite("Normal"));
                }
                else
                {
                    ShowSprite(SpriteFactory.Instance.CreateBrickSprite("Used"));
                }
            }
            else if (state is Destroyed)
            {
                ShowSprite(SpriteFactory.Instance.CreateBrickSprite("Destroyed"));
            }
            else
            {
                ShowSprite(SpriteFactory.Instance.CreateBrickSprite("Normal"));
            }
        }
        internal bool HasItem { get { return item != null || items.Count != 0; } }
        internal bool HasInitialItem { get { return hasInitialItem; } }
        private void LoadItem()
        {
            if (items.Count != 0)
            {
                item = items[0];
                items.RemoveAt(0);
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

        public Brick(IGameWorld world, Point location, IList<IGameObject> items, bool isHidden = false) : base(world, location, new Point(32, 32))
        {
            if (isHidden)
            {
                state = new Hidden(this);
            }
            else
            {
                state = new Normal(this);
            }
            this.items = items;
            if (items != null && items.Count != 0)
            {
                LoadItem();
                hasInitialItem = true;
            }
            UpdateSprite();
        }


        public void Bump(Mario mario)
        {
            State.Bump(mario);

        }

        public void BumpMove(int delta)
        {
            Move(new Point(0, delta));

        }

        public void ReleaseNextItem()
        {
            if (item != null)
            {
                World.AddObject(item);
                LoadItem();
            }
        }
    }
}

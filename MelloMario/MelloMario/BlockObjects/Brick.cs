using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.BrickStates;
using MelloMario.MarioObjects;
using MelloMario.Theming;

namespace MelloMario.BlockObjects
{
    class Brick : BaseCollidableObject
    {
        private IBlockState state;
        private IGameObject item;
        private bool isHidden;
        private bool hasInitialItem;

        public bool HasInitialItem
        {
            get
            {
                return hasInitialItem;
            }
        }

        public void Initialize()
        {
            if (isHidden)
            {
                state = new Hidden(this);
            }
            else
            {
                state = new Normal(this);
            }
            hasInitialItem = GameDatabase.HasItemEnclosed(this);
            if (hasInitialItem)
            {
                LoadItem();
            }
            UpdateSprite();
        }
        private void UpdateSprite()
        {
            if (state is Hidden)
            {
                HideSprite();
            }
            else
            {
                ShowSprite(SpriteFactory.Instance.CreateBrickSprite("Normal"));
            }
        }


        private void LoadItem()
        {
            if (GameDatabase.HasItemEnclosed(this))
            {
                item = GameDatabase.GetEnclosedItems(this)[0];
                GameDatabase.GetEnclosedItems(this).RemoveAt(0);
            }
            else
            {
                item = null;
            }
        }

        protected override void OnUpdate(int time)
        {
            state.Update(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode)
        {
        }

        protected override void OnCollideWorld(CollisionMode mode)
        {
        }

        protected override void OnDraw(int time, Rectangle viewport, ZIndex zIndex)
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

        public Brick(IGameWorld world, Point location) : this(world, location, false)
        {
        }

        public Brick(IGameWorld world, Point location, bool isHidden = false) : base(world, location, new Point(32, 32))
        {
            this.isHidden = isHidden;
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
            LoadItem();
            if (item != null)
            {
                World.Add(item);
            }
        }
    }
}

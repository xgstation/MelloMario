using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.QuestionStates;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects
{
    class Question : BaseCollidableObject
    {
        private IBlockState state;
        private IList<IGameObject> items;
        private IGameObject item;

        private void UpdateSprite()
        {
            if (state is Hidden)
            {
                HideSprite();
            }
            else if (HasItem)
            {
                ShowSprite(SpriteFactory.Instance.CreateQuestionSprite("Normal"));
            }
            else
            {
                ShowSprite(SpriteFactory.Instance.CreateQuestionSprite("Used"));
            }
        }
        public bool HasItem { get { return item != null || items.Count != 0; } }
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

        protected override void OnSeen(IPlayer player, CollisionMode mode)
        {
        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
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

        public Question(IGameWorld world, Point location, bool isHidden = false) : this(world, location, new List<IGameObject>(), isHidden)
        {
        }

        public Question(IGameWorld world, Point location, IList<IGameObject> items, bool isHidden = false) : base(world, location, new Point(32, 32))
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
            LoadItem();
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
                World.Add(item);
                LoadItem();
            }
        }
    }
}

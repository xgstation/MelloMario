using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.BrickStates;
using MelloMario.MarioObjects;
using System;

namespace MelloMario.BlockObjects
{
    class Brick : BaseGameObject
    {
        private IBlockState state;
        private string items;

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

        protected override void OnSimulation(GameTime time)
        {
            state.Update(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, ZIndex zIndex)
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
        public Brick(IGameWorld world, Point location, bool isHidden = false) : this(world, location, new Queue<IGameObject>(), isHidden)
        {
        }

        public Brick(IGameWorld world, Point location, Queue<IGameObject> items, bool isHidden = false) : base(world, location, new Point(32, 32))
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

            this.items = items.ToString();
        }
        public Brick(IGameWorld world, Point location, Tuple<bool, string> Property) : base(world, location, new Point(32,32))
        {
            if (Property.Item1)
            {
                state = new Hidden(this);
            }
            else
            {
                state = new Normal(this);
            }
            items = Property.Item2;
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

            throw new NotImplementedException();
            //if (items.Count == 0)
            //{
            //    return false;
            //}
            //else
            //{
            //    World().AddObject(items.Dequeue());

            //    return (items.Count != 0);
            //}
        }
    }
}

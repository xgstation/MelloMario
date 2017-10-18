using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.BrickStates;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects
{
    class Brick : BaseGameObject
    {
        private IBlockState state;
        private Queue<IGameObject> items;

        private void OnStateChanged()
        {
            if (state is Hidden)
            {
                HideSprite();
            }
            else if(state is Destroyed)
            {
                ShowSprite(SpriteFactory.Instance.CreateBrickSprite("DestroyedRT"));
                //ShowSprite(SpriteFactory.Instance.CreateBrickSprite("DestroyedLT"));
                //ShowSprite(SpriteFactory.Instance.CreateBrickSprite("DestroyedRB"));
                //ShowSprite(SpriteFactory.Instance.CreateBrickSprite("DestroyedLB"));

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

        protected override void OnCollision(IGameObject target, CollisionMode mode)
        {
            if (target is Mario && mode == CollisionMode.Bottom)
            {
                Bump((Mario)target);
            }
        }

        protected override void OnOut(CollisionMode mode)
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
                OnStateChanged();
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
            OnStateChanged();

            this.items = items;
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
            if (items.Count == 0)
            {
                return false;
            }
            else
            {
                World().AddObject(items.Dequeue());

                return true;
            }
        }
    }
}

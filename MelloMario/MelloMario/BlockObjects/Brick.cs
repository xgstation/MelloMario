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
        private Queue<char> items;
        private int elapsed;
        private void OnStateChanged()
        {
            if (state is Hidden)
            {
                HideSprite();
            }
            else if(state is Destroyed)
            {
                ShowSprite(SpriteFactory.Instance.CreateBrickSprite("DestroyedRT"));
                ShowSprite(SpriteFactory.Instance.CreateBrickSprite("DestroyedLT"));
                ShowSprite(SpriteFactory.Instance.CreateBrickSprite("DestroyedRB"));
                ShowSprite(SpriteFactory.Instance.CreateBrickSprite("DestroyedLB"));

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
            World().RemoveObject(this);
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
        public Queue<char> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
            }
        }
        public Brick(IGameWorld world, Point location, bool isHidden, Queue<char> items) : base(world, location, new Point(32, 32))
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
            OnStateChanged();
        }

        public Brick(IGameWorld world, Point location, bool isHidden) : this(world, location, isHidden, new Queue<char>())
        {

        }
        public Brick(IGameWorld world, Point location) : this(world, location, false, new Queue<char>())
        {

        }

        protected override void ShowSprite(ISprite newSprite, ResizeModeX modeX = ResizeModeX.Center, ResizeModeY modeY = ResizeModeY.Bottom)
        {

            base.ShowSprite(newSprite,modeX,modeY);
        }

        public void Show()
        {
            State.Show();
        }
        public void Hide()
        {
            State.Hide();
        }
        public void Bump(Mario mario)
        {
            State.Bump(mario);
        }

        public void ReleaseNextItem()
        {
            if (items.Count != 0)
            {
                switch (items.Dequeue())
                {
                    case 'C':
                        World().AddObject(new ItemObjects.Coin(World(), Location(), true));
                        break;
                    case '1':
                        World().AddObject(new ItemObjects.OneUpMushroom(World(), Location(), true));
                        break;
                    case 'R':
                        World().AddObject(new ItemObjects.FireFlower(World(), Location(), true));
                        break;
                    case 'M':
                        World().AddObject(new ItemObjects.SuperMushroom(World(), Location(), true));
                        break;
                    case 'T':
                        World().AddObject(new ItemObjects.Star(World(), Location(), true));
                        break;
                    default:
                        break;
                }
                if (items.Count == 0)
                    state = new Used(this);
            }
        }
    }
}

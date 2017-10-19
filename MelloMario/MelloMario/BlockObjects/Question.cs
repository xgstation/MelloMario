using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.QuestionStates;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects
{
    class Question : BaseGameObject
    {
        private IBlockState state;
        private Queue<IGameObject> items;
        private CollisionMode previousMode;
        private bool isFromTop;
        private void UpdateSprite()
        {
            if (state is Hidden)
            {
                HideSprite();
            }
            else
            {
                ShowSprite(SpriteFactory.Instance.CreateQuestionSprite(state.GetType().Name));
            }
        }

        protected override void OnSimulation(GameTime time)
        {
            state.Update(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionCornerMode corner, CollisionCornerMode cornerPassive)
        {
            if (target is Mario mario)
            {
                if (mode == CollisionMode.Bottom && cornerPassive == CollisionCornerMode.Center)
                {
                    isFromTop = CollisionMode.InnerTop == previousMode || CollisionMode.InnerBottom == previousMode || CollisionMode.Top == previousMode;
                    if (state is Hidden)
                    {
                        if (!isFromTop)
                        {
                            Bump(mario);
                        }
                    }
                    else
                    {
                        Bump(mario);
                    }
                }
                previousMode = mode;
            }
        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time)
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

        public Question(IGameWorld world, Point location, bool isHidden = false) : this(world, location, new Queue<IGameObject>(), isHidden)
        {
        }

        public Question(IGameWorld world, Point location, Queue<IGameObject> items, bool isHidden = false) : base(world, location, new Point(32, 32))
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

            this.items = items;
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

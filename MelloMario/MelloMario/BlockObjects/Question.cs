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
        private Queue<char> items;

        private void OnStateChanged()
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

        public Question(IGameWorld world, Point location, bool isHidden, Queue<char> items) : base(world, location, new Point(32,32))
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

        public Question(IGameWorld world, Point location, bool isHidden) : this(world, location, isHidden, new Queue<char>())
        {

        }
        public Question(IGameWorld world, Point location) : this(world, location, false, new Queue<char>())
        {

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
            }
            if (items.Count == 0)
                state = new Used(this);
        }
    }
}

using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.StarStates;
using MelloMario.MarioObjects;

namespace MelloMario.ItemObjects
{
    class Star : BasePhysicalObject
    {
        private IItemState state;
        private bool goingRight;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateStarSprite());
        }

        protected override void OnSimulation(GameTime time)
        {

            ApplyGravity();
            state.Update(time);
            if (state is Normal)
            {
                if (goingRight)
                    Move(new Point(3, 0));
                else
                    Move(new Point(-3, 0));
            }
            base.OnSimulation(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionCornerMode corner, CollisionCornerMode cornerPassive)
        {
            switch (target.GetType().Name)
            {
                case "Mario":
                    if (state is Normal)
                        Collect();
                    break;
                case "Brick":
                case "Question":
                case "Floor":
                case "Pipeline":
                case "Stair":
                    // TODO: check against hidden
                    Bounce(mode, new Vector2());
                    if (mode == CollisionMode.Left || mode == CollisionMode.InnerLeft && corner == CollisionCornerMode.Center)
                    {
                        Bounce(mode, new Vector2(), 1);
                        goingRight = true;
                    }
                    else if (mode == CollisionMode.Right || mode == CollisionMode.InnerRight && corner == CollisionCornerMode.Center)
                    {
                        Bounce(mode, new Vector2(), 1);
                        goingRight = false;
                    }
                    if (mode == CollisionMode.Bottom || mode == CollisionMode.InnerBottom && corner == CollisionCornerMode.Center)
                        ApplyForce(new Vector2(0, -160f));
                    break;
            }
        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, ZIndex zIndex)
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
                UpdateSprite();
            }
        }

        public Star(IGameWorld world, Point location, bool isUnveil) : base(world, location, new Point(32, 32), 32)
        {
            goingRight = true;
            if (isUnveil)
            {
                state = new Unveil(this);
            }
            else
            {
                state = new Normal(this);
            }
            UpdateSprite();
        }
        public Star(IGameWorld world, Point location) : this(world, location, false)
        {
        }

        public void Show()
        {
            State.Show();
        }
        public void Collect()
        {
            RemoveSelf();
            //State.Collect();
        }

        public void UnveilMove(int delta)
        {
            Move(new Point(0, delta));
        }
    }
}

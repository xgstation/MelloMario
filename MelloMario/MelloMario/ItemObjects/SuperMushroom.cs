using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.SuperMushroomStates;
using MelloMario.MarioObjects;
using MelloMario.BlockObjects;

namespace MelloMario.ItemObjects
{
    class SuperMushroom : BasePhysicalObject
    {
        private const int H_SPEED = 2;
        private IItemState state;
        private bool goingRight;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateSuperMushroomSprite());
        }

        protected override void OnUpdate(GameTime time)
        {
            ApplyGravity();

            state.Update(time);
            if (state is Normal)
            {
                if (goingRight)
                    Move(new Point(H_SPEED, 0));
                else
                    Move(new Point(-1 * H_SPEED, 0));
            }
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
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
                    if (mode == CollisionMode.Left || mode == CollisionMode.InnerLeft && corner == CornerMode.Center)
                    {
                        Bounce(mode, new Vector2(), 1);
                        goingRight = true;
                    }
                    else if (mode == CollisionMode.Right || mode == CollisionMode.InnerRight && corner == CornerMode.Center)
                    {
                        Bounce(mode, new Vector2(), 1);
                        goingRight = false;
                    }
                    break;
            }
        }

        protected override void OnOut(CollisionMode mode)
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

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
        }

        public SuperMushroom(IGameWorld world, Point location, Point marioLocation, bool isUnveil) : base(world, location, new Point(32, 32), 32)
        {
            if (marioLocation.X < location.X)
                goingRight = false;
            else
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
        public SuperMushroom(IGameWorld world, Point location, Point marioLocation) : this(world, location, marioLocation, false)
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

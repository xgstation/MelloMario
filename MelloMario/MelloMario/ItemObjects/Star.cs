using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.StarStates;
using MelloMario.MarioObjects;
using MelloMario.BlockObjects;
using System.Diagnostics;

namespace MelloMario.ItemObjects
{
    class Star : BasePhysicalObject
    {
        private const int H_SPEED = 3;
        private IItemState state;
        private bool goingRight;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateStarSprite());
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
            //Debug.WriteLine(target.GetType().Name);
            switch (target.GetType().Name)
            {
                case "PlayerMario":
                    if (state is Normal)
                        Collect();
                    break;
                case "Brick":
                    if (((Brick)target).State is BlockObjects.BrickStates.Hidden)
                        break;
                    goto case "Stair";
                case "Question":
                    if (((Question)target).State is BlockObjects.QuestionStates.Hidden)
                        break;
                    goto case "Stair";
                case "Floor":
                case "Pipeline":
                case "Stair":
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
                    if (mode == CollisionMode.Bottom || mode == CollisionMode.InnerBottom && corner == CornerMode.Center)
                        ApplyForce(new Vector2(0, -160f));
                    break;
            }
        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
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

        public Star(IGameWorld world, Point location, Point marioLocation, bool isUnveil) : base(world, location, new Point(32, 32), 32)
        {
            if (marioLocation.X < location.X)
                goingRight = true;
            else
                goingRight = false;
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
        public Star(IGameWorld world, Point location, Point marioLocation) : this(world, location, marioLocation, false)
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

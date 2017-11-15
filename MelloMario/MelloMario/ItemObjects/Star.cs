using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.StarStates;
using MelloMario.BlockObjects;
using MelloMario.SplashObjects;
using MelloMario.Theming;

namespace MelloMario.ItemObjects
{
    class Star : BasePhysicalObject
    {
        private const int H_SPEED = 3;
        private IItemState state;
        private bool goingRight;
        private bool collected;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateStarSprite());
        }

        protected override void OnUpdate(int time)
        {
            state.Update(time);
        }

        protected override void OnSimulation(int time)
        {
            if (state is Normal)
            {
                ApplyGravity();

                if (goingRight)
                {
                    Move(new Point(H_SPEED, 0));
                }
                else
                {
                    Move(new Point(-1 * H_SPEED, 0));
                }
            }

            base.OnSimulation(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionMode modePassive, CornerMode corner, CornerMode cornerPassive)
        {
            if (state is Normal)
            {
                switch (target.GetType().Name)
                {
                    case "PlayerMario":
                        if (state is Normal)
                        {
                            Collect();
                        }
                        break;
                    case "Brick":
                        if (((Brick) target).State is BlockObjects.BrickStates.Hidden)
                        {
                            break;
                        }
                        goto case "Stair";
                    case "Question":
                        if (((Question) target).State is BlockObjects.QuestionStates.Hidden)
                        {
                            break;
                        }
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
                        {
                            ApplyForce(new Vector2(0, -160f));
                        }
                        break;
                }
            }
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnDraw(int time, Rectangle viewport)
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

        public Star(IGameWorld world, Point location, Point marioLocation, Listener listener, bool isUnveil = true) : base(world, location, listener, new Point(32, 32), 32)
        {
            collected = false;

            if (marioLocation.X < location.X)
            {
                goingRight = true;
            }
            else
            {
                goingRight = false;
            }
            if (isUnveil)
            {
                state = new Unveil(this);
                UpdateSprite();
                RemoveSelf();
            }
            else
            {
                state = new Normal(this);
                UpdateSprite();
            }
        }

        public Star(IGameWorld world, Point location, Point marioLocation, Listener listener) : this(world, location, marioLocation, listener, false) { }

        public void Collect()
        {
            if (!collected)
            {
                ScorePoints(GameConst.SCORE_POWER_UP);
                new PopingUpPoints(world, Boundary.Location, GameConst.SCORE_POWER_UP);
            }
            collected = true;
            RemoveSelf();
            //State.Collect();
        }

        public void UnveilMove(int delta)
        {
            Move(new Point(0, delta));
        }
    }
}

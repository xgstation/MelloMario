using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.SuperMushroomStates;
using MelloMario.BlockObjects;
using MelloMario.Theming;

namespace MelloMario.ItemObjects
{
    class SuperMushroom : BasePhysicalObject
    {
        private const int H_SPEED = 2;
        private IItemState state;
        private bool collected;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateSuperMushroomSprite());
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

                if (Facing == FacingMode.right)
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

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
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
                        Facing = FacingMode.right;
                    }
                    else if (mode == CollisionMode.Right || mode == CollisionMode.InnerRight && corner == CornerMode.Center)
                    {
                        Bounce(mode, new Vector2(), 1);
                        Facing = FacingMode.left;
                    }
                    break;
            }
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode)
        {
        }

        protected override void OnCollideWorld(CollisionMode mode)
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

        protected override void OnDraw(int time, Rectangle viewport)
        {
        }


        public SuperMushroom(IGameWorld world, Point location, Point marioLocation, Listener listener) : this(world, location, marioLocation, listener, false) { }
        public SuperMushroom(IGameWorld world, Point location, Listener listener) : this(world, location, GameDatabase.GetCharacterLocation(), listener) { }
        public SuperMushroom(IGameWorld world, Point location, Point marioLocation, Listener listener, bool isUnveil = true) : base(world, location, listener, new Point(32, 32), 32)
        {

            collected = false;
            if (marioLocation.X < location.X)
            {
                Facing = FacingMode.left;
            }
            else
            {
                Facing = FacingMode.right;
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

        public void Collect()
        {
            if(!collected)
                ScorePoints(GameConst.SCORE_POWER_UP);
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

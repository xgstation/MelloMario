using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.SuperMushroomStates;
using MelloMario.BlockObjects;
using MelloMario.Containers;
using MelloMario.UIObjects;
using MelloMario.Theming;

namespace MelloMario.ItemObjects
{
    class SuperMushroom : BasePhysicalObject
    {
        private IItemState state;
        private bool collected;


        public IGameObject GetFireFlower()
        {
            // TODO: listener?
            return GameObjectFactory.Instance.CreateGameObject("FireFlowerUnveil", World, Boundary.Location, null);
        }

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

                if (Facing == FacingMode.left)
                {
                    SetHorizontalVelocity(-GameConst.VELOCITY_SUPER_MUSHROOM);
                }
                else
                {
                    SetHorizontalVelocity(GameConst.VELOCITY_SUPER_MUSHROOM);
                }
            }

            base.OnSimulation(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionMode modePassive, CornerMode corner, CornerMode cornerPassive)
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
                    if (mode == CollisionMode.Top || mode == CollisionMode.Bottom)
                    {
                        Bounce(mode, new Vector2());
                    }
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

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
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

        protected override void OnDraw(int time)
        {
        }


        public SuperMushroom(IGameWorld world, Point location, Point marioLocation, Listener listener) : this(world, location, marioLocation, listener, false) { }

        //This suppression exists because this constructor is inderectly used by the json parser.
        //removing this constructor will cause a runtime error when trying to read in the level.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
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
            if (!collected)
            {
                ScorePoints(GameConst.SCORE_POWER_UP);
                new PopingUpPoints(World, Boundary.Location, GameConst.SCORE_POWER_UP);
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

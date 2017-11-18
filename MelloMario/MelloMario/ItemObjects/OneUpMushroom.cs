using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.OneUpMushroomStates;
using MelloMario.Theming;
using Microsoft.Xna.Framework.Audio;
using MelloMario.Sounds;

namespace MelloMario.ItemObjects
{
    class OneUpMushroom : BasePhysicalObject
    {
        private IItemState state;
        private SoundEffectInstance oneupMushCollectSound;
        private bool collected;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateOneUpMushroomSprite());
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
                    SetHorizontalVelocity(-GameConst.VELOCITY_ONE_UP_MUSHROOM);
                }
                else
                {
                    SetHorizontalVelocity(GameConst.VELOCITY_ONE_UP_MUSHROOM);
                }
            }

            base.OnSimulation(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionMode modePassive, CornerMode corner, CornerMode cornerPassive)
        {
            switch (target.GetType().Name)
            {
                case "MarioCharacter":
                    if (state is Normal)
                    {
                        Collect();
                    }
                    break;
                case "Brick":
                case "Question":
                case "Floor":
                case "Pipeline":
                case "Stair":
                    // TODO: check against hidden
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

        protected override void OnDraw(int time)
        {
        }

        public IItemState State
        {
            set
            {
                state = value;
                UpdateSprite();
            }
        }

        public OneUpMushroom(IGameWorld world, Point location, Point marioLocation, Listener listener, bool isUnveil) : base(world, location, listener, new Point(32, 32), 32)
        {
            oneupMushCollectSound = SoundController.OneUpCollect.CreateInstance();
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

        //This suppression exists because this constructor is inderectly used by the json parser.
        //removing this constructor will cause a runtime error when trying to read in the level.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public OneUpMushroom(IGameWorld world, Point location, Listener listener) : this(world, location, GameDatabase.GetCharacterLocation(), listener, false) { }
        public OneUpMushroom(IGameWorld world, Point location, Point marioLocation, Listener listener) : this(world, location, marioLocation, listener, false) { }

        public void Collect()
        {
            oneupMushCollectSound.Play();
            if (!collected)
            {
                ChangeLives();
                collected = true;
            }
            RemoveSelf();
            //State.Collect();
        }

        public void UnveilMove(int delta)
        {
            Move(new Point(0, delta));
        }
    }
}
namespace MelloMario.Objects.Items
{
    #region

    using System.Diagnostics.CodeAnalysis;
    using MelloMario.Factories;
    using MelloMario.Objects.Items.OneUpMushroomStates;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class OneUpMushroom : BasePhysicalObject
    {
        private bool collected;
        private IItemState state;

        public OneUpMushroom(
            IGameWorld world,
            Point location,
            IListener<IGameObject> listener,
            bool isUnveil = false) : base(world, location, listener, new Point(32, 32), 32)
        {
            collected = false;

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

        public IItemState State
        {
            set
            {
                state = value;
                UpdateSprite();
            }
        }

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
                    SetHorizontalVelocity(-Const.VELOCITY_ONE_UP_MUSHROOM);
                }
                else
                {
                    SetHorizontalVelocity(Const.VELOCITY_ONE_UP_MUSHROOM);
                }
            }

            base.OnSimulation(time);
        }

        protected override void OnCollision(
            IGameObject target,
            CollisionMode mode,
            CollisionMode modePassive,
            CornerMode corner,
            CornerMode cornerPassive)
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
                    else if (mode == CollisionMode.Right
                        || mode == CollisionMode.InnerRight && corner == CornerMode.Center)
                    {
                        Bounce(mode, new Vector2(), 1);
                        Facing = FacingMode.left;
                    }
                    break;
            }
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode, CollisionMode modePassive) { }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive) { }

        protected override void OnDraw(int time, SpriteBatch spriteBatch) { }

        public void Collect()
        {
            //TODO:Move this into soundcontroller
            //SoundManager.OneUpCollect.Play();
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

namespace MelloMario.Objects.Blocks
{
    #region

    using MelloMario.Factories;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Characters.MovementStates;
    using MelloMario.Sounds;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class Pipeline : BaseCollidableObject, ISoundable
    {
        private int elapsed;
        private Theming.Model model;
        private IPlayer switchingPlayer;

        public event SoundHandler SoundEvent;
        public SoundArgsBase SoundEventArgs { get; }
        public Pipeline(
            IWorld world,
            Point location,
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener,
            string type,
            Theming.Model model) : this(world, location, listener, soundListener, type)
        {
            SetModel(model);
        }

        public Pipeline(IWorld world, Point location, IListener<IGameObject> listener, IListener<ISoundable> soundListener, string type) : base(
            world,
            location,
            listener,
            new Point(32, 32))
        {
            Type = type;

            soundListener.Subscribe(this);
            SoundEventArgs = new SoundArgs();
            UpdateSprite();
        }

        public string Type { get; }

        private void SetModel(Theming.Model newModel)
        {
            model = newModel;
        }

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreatePipelineSprite(Type));
        }

        protected override void OnUpdate(int time)
        {
            if (switchingPlayer != null)
            {
                elapsed += time;

                if (elapsed > 500)
                {
                    IWorld world = model.LoadLevel(Database.GetEntranceIndex(this));

                    if (Database.IsPortal(this))
                    {
                        switchingPlayer.Spawn(world, Database.GetPortal(this).Boundary.Location);
                    }
                    else
                    {
                        switchingPlayer.Spawn(world, world.GetInitialPoint());
                    }

                    elapsed = 0;
                    switchingPlayer = null;
                }
            }
        }

        protected override void OnCollision(
            IGameObject target,
            CollisionMode mode,
            CollisionMode modePassive,
            CornerMode corner,
            CornerMode cornerPassive)
        {
            if (target is Mario mario && mode is CollisionMode.Top)
            {
                if (mario.MovementState is Crouching && Database.IsEntrance(this))
                {
                    //TODO:Add warping state to model to allow sound controller play warping sound
                    //SoundManager.Pipe.Play();
                    switch (Type)
                    {
                        case "LeftIn":
                            if (mario.Boundary.Center.X > Boundary.Center.X)
                            {
                                // TODO
                                // switchingPlayer = mario;
                            }
                            break;
                        case "RightIn":
                            if (mario.Boundary.Center.X < Boundary.Center.X)
                            {
                                // TODO
                                // switchingPlayer = mario;
                            }
                            break;
                    }

                    // TODO: mario.freeze
                }
            }
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
        }

    }
}

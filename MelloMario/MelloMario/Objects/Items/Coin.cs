namespace MelloMario.Objects.Items
{
    #region

    using System;
    using MelloMario.Factories;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Items.CoinStates;
    using MelloMario.Objects.Miscs;
    using MelloMario.Sounds.Effects;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    [Serializable]
    internal class Coin : BaseCollidableObject, ISoundable
    {
        public delegate void CoinHandler(Coin m, EventArgs e);

        private readonly EventArgs coinEventInfo;
        private bool collected;
        private IItemState state;

        public Coin(
            IWorld world,
            Point location,
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener,
            bool isUnveil = false) : base(world, location, null, new Point(32, 32))
        {
            listener.Subscribe(this);
            soundListener.Subscribe(this);
            collected = false;
            //eventually if coin needs to pass info put it in eventinfo
            coinEventInfo = EventArgs.Empty;
            SoundEventArgs = new SoundArgs();
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

        public ISoundArgs SoundEventArgs { get; }

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

        public event CoinHandler HandlerCoins;

        public event SoundHandler SoundEvent;

        public void Collect()
        {
            if (!collected)
            {
                collected = true;
                SoundEventArgs.SetMethodCalled();
                HandlerCoins?.Invoke(this, coinEventInfo);
                ScorePoints(Const.SCORE_COIN);
                World.Add(new PopingUpPoints(World, Boundary.Location, Const.SCORE_COIN));
            }
            RemoveSelf();
            //State.Collect();
        }

        public void UnveilMove(int delta)
        {
            Move(new Point(0, delta));
        }

        protected override void OnUpdate(int time)
        {
            SoundEvent?.Invoke(this, SoundEventArgs);
            state.Update(time);
        }

        protected override void OnCollision(
            IGameObject target,
            CollisionMode mode,
            CollisionMode modePassive,
            CornerMode corner,
            CornerMode cornerPassive)
        {
            if (target is Mario && state is Normal)
            {
                Collect();
            }
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateCoinSprite());
        }
    }
}

﻿namespace MelloMario.Objects.Items
{
    #region

    using System;
    using Characters;
    using CoinStates;
    using Factories;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Theming;
    using UserInterfaces;

    #endregion

    internal class Coin : BaseCollidableObject
    {
        public delegate void CoinHandler(Coin m, EventArgs e);

        private readonly EventArgs coinEventInfo;
        private bool collected;
        private IItemState state;

        public Coin(IGameWorld world, Point location, IListener listener, bool isUnveil = false) : base(world, location, listener, new Point(32, 32))
        {
            listener.Subscribe(this);
            collected = false;
            //eventually if coin needs to pass info put it in eventinfo
            coinEventInfo = null;
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

        public event CoinHandler HandlerCoins;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateCoinSprite());
        }

        protected override void OnUpdate(int time)
        {
            state.Update(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionMode modePassive, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is Mario && state is Normal)
            {
                Collect();
            }
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode, CollisionMode modePassive) { }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive) { }

        protected override void OnDraw(int time, SpriteBatch spriteBatch) { }

        public void Collect()
        {
            if (!collected)
            {
                HandlerCoins?.Invoke(this, coinEventInfo);
                ScorePoints(Const.SCORE_COIN);
                collected = true;
                new PopingUpPoints(World, Boundary.Location, Const.SCORE_COIN);
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

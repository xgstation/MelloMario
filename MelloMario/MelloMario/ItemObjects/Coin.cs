using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.CoinStates;
using MelloMario.MarioObjects;
using System;
using MelloMario.SplashObjects;
using MelloMario.Theming;

namespace MelloMario.ItemObjects
{
    class Coin : BaseCollidableObject
    {
        private IItemState state;
        public event CoinHandler HandlerCoins;
        private EventArgs coinEventInfo;
        public delegate void CoinHandler(Coin m, EventArgs e);
        private bool collected;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateCoinSprite());
        }

        protected override void OnUpdate(int time)
        {
            state.Update(time);
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is Mario && state is Normal)
            {
                Collect();
            }
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode)
        {
        }

        protected override void OnCollideWorld(CollisionMode mode)
        {
        }

        protected override void OnDraw(int time, Rectangle viewport)
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

        public Coin(IGameWorld world, Point location, Listener listener, bool isUnveil = false) : base(world, location, listener, new Point(32, 32))
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

        public void Collect()
        {
            if (!collected)
            {
                HandlerCoins?.Invoke(this, coinEventInfo);
                ScorePoints(GameConst.SCORE_COIN);
                collected = true;
                new PopingUpPoints(world, Boundary.Location, GameConst.SCORE_COIN);
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

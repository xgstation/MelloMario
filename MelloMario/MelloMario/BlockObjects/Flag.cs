using MelloMario.Factories;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using System;
using MelloMario.SplashObjects;

namespace MelloMario.BlockObjects
{
    class Flag : BaseCollidableObject
    {
        private bool top;
        private int height, maxHeight;
        public event TimeScoreHandler HandlerTimeScore;
        private EventArgs eventInfo;
        public delegate void TimeScoreHandler(Flag m, EventArgs e);

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateFlagSprite(top));
        }

        protected override void OnUpdate(int time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is MarioObjects.PlayerMario mario)
            {
                if (mario.Active && !world.FlagIsTouched)
                {
                    if (top)
                    {
                        ChangeLives(1);
                    }
                    eventInfo = null;
                    HandlerTimeScore?.Invoke(this, eventInfo);
                    ScorePoints(GameConst.SCORE_FLAG_MAX * height / maxHeight);
                    world.FlagIsTouched = true;
                    new PopingUpPoints(world, Boundary.Location, GameConst.SCORE_FLAG_MAX * height / maxHeight);
                    mario.FlagPole();

                    //TODO: trigger game win
                }
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

        public Flag(IGameWorld world, Point location, Listener listener, int height, int maxHeight) : base(world, location, listener, new Point(32, 32))
        {
            listener.Subscribe(this);
            this.height = height;
            this.maxHeight = maxHeight;
            top = height == maxHeight;
            UpdateSprite();
        }
    }
}

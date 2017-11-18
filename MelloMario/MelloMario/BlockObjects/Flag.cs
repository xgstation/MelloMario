using MelloMario.Factories;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using System;
using MelloMario.UIObjects;
using Microsoft.Xna.Framework.Graphics;

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

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionMode modePassive, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is MarioObjects.MarioCharacter mario)
            {
                if (mario.Active)
                {
                    if (top)
                    {
                        ChangeLives();
                    }
                    eventInfo = null;
                    HandlerTimeScore?.Invoke(this, eventInfo);
                    ScorePoints(GameConst.SCORE_FLAG_MAX * height / maxHeight);
                    new PopingUpPoints(World, Boundary.Location, GameConst.SCORE_FLAG_MAX * height / maxHeight);
                    mario.FlagPole();
                }
            }
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
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

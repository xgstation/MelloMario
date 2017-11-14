using MelloMario.Factories;
using MelloMario.Theming;
using Microsoft.Xna.Framework;

namespace MelloMario.BlockObjects
{
    class Flag : BaseCollidableObject
    {
        private bool top;
        private int height, maxHeight;
        private bool collected;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateFlagSprite(top));
        }

        protected override void OnUpdate(int time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            //trigger game win and add points based on collision locations
            //temp: hurts mario just to demonstrate that a collision was detected.
            if (target is MarioObjects.PlayerMario)
            {
                ((MarioObjects.PlayerMario) target).Downgrade();
                if(!collected)
                {
                    if (top)
                        ChangeLives(1);
                    ScorePoints((int)(1f * height / maxHeight * GameConst.SCORE_FLAG_MAX));
                    collected = true;
                }
                //TODO: trigger game win
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
            collected = false;
            this.height = height;
            this.maxHeight = maxHeight;
            top = height == maxHeight;
            UpdateSprite();
        }
    }
}

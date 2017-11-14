using MelloMario.Factories;
using MelloMario.Theming;
using Microsoft.Xna.Framework;

namespace MelloMario.BlockObjects
{
    class Flag : BaseCollidableObject
    {
        private bool top;
        private bool touched;
        private int height, maxHeight;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateFlagSprite(top));
        }

        protected override void OnUpdate(int time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is MarioObjects.PlayerMario)
            {
                if (!touched)
                {
                    if (top)
                    {
                        ChangeLives(1);
                    }
                    ScorePoints(GameConst.SCORE_FLAG_MAX * height / maxHeight);
                    touched = true;

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
            this.height = height;
            this.maxHeight = maxHeight;
            top = height == maxHeight;
            UpdateSprite();
        }
    }
}

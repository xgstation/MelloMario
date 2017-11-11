using Microsoft.Xna.Framework;
using MelloMario.Factories;

namespace MelloMario.BlockObjects
{
    class Stair : BaseCollidableObject
    {
        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateStairSprite());
        }

        protected override void OnUpdate(int time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
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

        public Stair(IGameWorld world, Point location, bool isHidden = false) : base(world, location, new Point(32, 32))
        {
            if (isHidden)
            {
                HideSprite();
            }
            else
            {
                UpdateSprite();
            }
        }
    }
}

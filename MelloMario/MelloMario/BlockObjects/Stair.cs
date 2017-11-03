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

        protected override void OnUpdate(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
        }

        protected override void OnSeen(IPlayer player, CollisionMode mode)
        {
        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
        }

        public Stair(IGameWorld world, Point location) : base(world, location, new Point(32, 32))
        {
            UpdateSprite();
        }
    }
}

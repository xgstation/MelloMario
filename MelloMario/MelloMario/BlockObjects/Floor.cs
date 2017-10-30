using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects
{
    class Floor : BaseCollidableObject
    {
        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateFloorSprite());
        }

        protected override void OnUpdate(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
        }

        protected override void OnOut(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
        }

        public Floor(IGameWorld world, Point location) : base(world, location, new Point(32, 32))
        {
            UpdateSprite();
        }
    }
}

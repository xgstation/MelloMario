using Microsoft.Xna.Framework;
using MelloMario.Factories;

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

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode)
        {
        }

        protected override void OnCollideWorld(CollisionMode mode)
        {
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
        }

        public Floor(IGameWorld world, Point location) : this(world, location, false) { }
        public Floor(IGameWorld world, Point location, bool isHidden) : base(world, location, new Point(32, 32))
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

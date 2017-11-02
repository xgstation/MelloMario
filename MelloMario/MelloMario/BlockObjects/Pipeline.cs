using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects
{
    class Pipeline : BaseCollidableObject
    {
        private string type;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreatePipelineSprite(type));
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

        public Pipeline(IGameWorld world, Point location, string type) : base(world, location, new Point(32, 32))
        {
            this.type = type;
            UpdateSprite();
        }
    }
}

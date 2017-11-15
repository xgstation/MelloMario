using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.Theming;

namespace MelloMario.BlockObjects
{
    class Floor : BaseCollidableObject
    {
        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateFloorSprite());
        }

        protected override void OnUpdate(int time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionMode modePassive, CornerMode corner, CornerMode cornerPassive)
        {
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        protected override void OnDraw(int time, Rectangle viewport)
        {
        }

        public Floor(IGameWorld world, Point location, Listener listener, bool isHidden = false) : base(world, location, listener, new Point(32, 32))
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

using MelloMario.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects
{
    internal class Stair : BaseCollidableObject
    {
        public Stair(IGameWorld world, Point location, IListener listener, bool isHidden = false) : base(world, location, listener, new Point(32, 32))
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

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateStairSprite());
        }

        protected override void OnUpdate(int time) { }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionMode modePassive, CornerMode corner, CornerMode cornerPassive) { }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode, CollisionMode modePassive) { }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive) { }

        protected override void OnDraw(int time, SpriteBatch spriteBatch) { }
    }
}

using Microsoft.Xna.Framework;
using MelloMario.Factories;

namespace MelloMario.MiscObjects
{
    class Background : BaseGameObject
    {
        private string type;
        private ZIndex zIndex;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateSceneSprite(type, zIndex));
        }

        protected override void OnUpdate(GameTime time)
        {
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        public Background(IGameWorld world, Point location, string type, ZIndex zIndex) : base(world, location, new Point(32, 32))
        {
            this.type = type;
            this.zIndex = zIndex;
            UpdateSprite();
        }
    }
}

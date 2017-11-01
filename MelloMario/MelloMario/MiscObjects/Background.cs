using Microsoft.Xna.Framework;
using MelloMario.Factories;

namespace MelloMario.MiscObjects
{
    class Background : BaseGameObject
    {
        private string type;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateSceneSprite(type));
        }

        protected override void OnUpdate(GameTime time)
        {
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
        }

        protected override void OnSimulation(GameTime time)
        {
            throw new System.NotImplementedException();
        }

        public Background(IGameWorld world, Point location, string type) : base(world, location, new Point(32, 32))
        {
            this.type = type;

            UpdateSprite();
        }
    }
}

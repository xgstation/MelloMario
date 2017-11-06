﻿using Microsoft.Xna.Framework;
using MelloMario.Factories;

namespace MelloMario.MiscObjects
{
    class Background : BaseGameObject
    {
        private string type;
        private ZIndex targetZIndex;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateSceneSprite(type, targetZIndex));
        }

        protected override void OnUpdate(GameTime time)
        {
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
        }

        public Background(IGameWorld world, Point location, string type, ZIndex zIndex) : base(world, location, new Point(32, 32))
        {
            this.type = type;
            targetZIndex = zIndex;
            UpdateSprite();
        }
    }
}

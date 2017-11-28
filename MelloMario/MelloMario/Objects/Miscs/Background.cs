namespace MelloMario.Objects.Miscs
{
    #region

    using System;
    using MelloMario.Factories;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    [Serializable]
    internal class Background : BaseGameObject
    {
        private readonly ZIndex targetZIndex;
        private readonly string type;

        public Background(IWorld world, Point location, string type, ZIndex zIndex) : base(
            world,
            location,
            new Point(32, 32))
        {
            this.type = type;
            targetZIndex = zIndex;
            UpdateSprite();
        }

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateSceneSprite(type, targetZIndex));
        }

        protected override void OnUpdate(int time)
        {
        }

        protected override void OnSimulation(int time)
        {
        }
    }
}

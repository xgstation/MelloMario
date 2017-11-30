namespace MelloMario.Objects.Miscs
{
    #region

    using System;
    using MelloMario.Factories;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Background : BaseGameObject
    {
        private readonly ZIndex targetZIndex;
        public string BackGroundType { get; }

        public Background(IWorld world, Point location, string backGroundType, ZIndex zIndex) : base(
            world,
            location,
            new Point(32, 32))
        {
            BackGroundType = backGroundType;
            targetZIndex = zIndex;
            UpdateSprite();
        }

        protected override void OnUpdate(int time)
        {
        }

        protected override void OnSimulation(int time)
        {
        }

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateSceneSprite(BackGroundType, targetZIndex));
        }
    }
}

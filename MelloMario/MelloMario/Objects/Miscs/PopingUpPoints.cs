﻿namespace MelloMario.Objects.Miscs
{
    #region

    using MelloMario.Factories;
    using Microsoft.Xna.Framework;

    #endregion

    internal class PopingUpPoints : BaseGameObject
    {
        private int elapsed;

        public PopingUpPoints(IWorld world, Point location, int points) : base(world, location, new Point())
        {
            ShowSprite(SpriteFactory.Instance.CreateTextSprite(points.ToString(), 10f));
        }

        protected override void OnUpdate(int time)
        {
            elapsed += time;
            if (elapsed < 1000)
            {
                Relocate(Boundary.Location + new Point(0, -2));
            }
            else
            {
                World.Remove(this);
            }
        }

        protected override void OnSimulation(int time)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelloMario.Factories;
using Microsoft.Xna.Framework;

namespace MelloMario.SplashObjects
{
    class PopingUpPoints : BaseGameObject
    {
        private int elapsed;
        public PopingUpPoints(IGameWorld world, Point location, int points) : base(world, location, new Point())
        {
            ShowSprite(SpriteFactory.Instance.CreateTextSprite(points.ToString()));
        }


        protected override void OnUpdate(int time)
        {
            elapsed += time;
            if (elapsed < 1000)
            {
                Relocate(new Point(Boundary.X, Boundary.Y - 2));
            }
            else
            {
                world.Remove(this);
            }
        }

        protected override void OnSimulation(int time)
        {

        }

        protected override void OnDraw(int time, Rectangle viewport)
        {

        }

    }
}

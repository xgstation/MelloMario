using MelloMario.Factories;
using Microsoft.Xna.Framework;

namespace MelloMario.UIObjects
{
    class PopingUpPoints : BaseGameObject
    {
        private int elapsed;

        protected override void OnUpdate(int time)
        {
            elapsed += time;
            if (elapsed < 1000)
            {
                Relocate(Boundary.Location + new Point(0, -2));
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

        public PopingUpPoints(IGameWorld world, Point location, int points) : base(world, location, new Point())
        {
            ShowSprite(SpriteFactory.Instance.CreateTextSprite(points.ToString(), 10f));
        }
    }
}

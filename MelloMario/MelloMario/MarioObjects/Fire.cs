using MelloMario.Factories;
using Microsoft.Xna.Framework;
using MelloMario.Theming;

namespace MelloMario.MarioObjects
{
    class Fire : BasePhysicalObject
    {
        protected override void OnSimulation(int time)
        {
            base.OnSimulation(time);
        }

        protected override void OnUpdate(int time)
        {
            //throw new NotImplementedException();
        }

        protected override void OnDraw(int time, Rectangle viewport)
        {
            //throw new NotImplementedException();
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is Mario)
            {
                return;
            }
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode)
        {
            //throw new NotImplementedException();
        }

        protected override void OnCollideWorld(CollisionMode mode)
        {
            //throw new NotImplementedException();
        }

        public Fire(IGameWorld world, Point location, Listener listener, bool isRight = true) : base(world, location, listener, new Point(16, 16), 32, 5f)
        {
            ShowSprite(SpriteFactory.Instance.CreateFireSprite());
        }

    }
}

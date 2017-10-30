using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MelloMario
{
    class Camera : BaseGameObject
    {
        protected override void OnUpdate(GameTime time)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnSimulation(GameTime time)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
            throw new System.NotImplementedException();
        }
        
        public Camera(IGameWorld world, Point location, Point size) : base(world, location, size)
        {
            //
        }
    }
}

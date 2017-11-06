using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MelloMario.EnemyObjects
{
    class Piranha : BasePhysicalObject
    {
        private float hideTime;
        public Piranha(IGameWorld world, Point location, Point size, float hideTime, float pixelScale) : base(world, location, size, pixelScale)
        {
            this.hideTime = hideTime;
            //TODO: Implment Piranha sprite, factory method and update interface.
            //ShowSprite(Factories.SpriteFactory.Instance.createPiranhaSprite());
        }

        protected override void OnUpdate(GameTime time)
        {
            throw new NotImplementedException();
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
            throw new NotImplementedException();
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            throw new NotImplementedException();
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode)
        {
            throw new NotImplementedException();
        }

        protected override void OnCollideWorld(CollisionMode mode)
        {
            throw new NotImplementedException();
        }
    }
}

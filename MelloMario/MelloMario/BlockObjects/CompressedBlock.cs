using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelloMario.Factories;
using MelloMario.Sprites;
using Microsoft.Xna.Framework;

namespace MelloMario.BlockObjects
{
    class CompressedBlock : BaseCollidableObject
    {
        public CompressedBlock(IGameWorld world, Point location, Point fullSize, Type type) : base(world, location, fullSize)
        {
            ShowSprite(SpriteFactory.Instance.CreateCompressedSprite(fullSize, type.Name));
        }

        protected override void OnUpdate(GameTime time)
        {
        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
        }

        protected override void OnSeen(IPlayer player, CollisionMode mode)
        {
        }

        protected override void OnOut(CollisionMode mode)
        {
        }
    }
}

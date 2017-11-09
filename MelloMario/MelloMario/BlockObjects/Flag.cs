using MelloMario.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.BlockObjects
{
    class Flag : BaseCollidableObject
    {
        private bool top;
        private int height, maxHeight;

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateFlagSprite(top));
        }

        protected override void OnUpdate(int time)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            //trigger game win and add points based on collision locations
            //temp: hurts mario just to demonstrate that a collision was detected.
            if (target is MarioObjects.PlayerMario)
            {
                ((MarioObjects.PlayerMario)target).Downgrade();
            }
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode)
        {
        }

        protected override void OnCollideWorld(CollisionMode mode)
        {
        }

        protected override void OnDraw(int time, Rectangle viewport, ZIndex zIndex)
        {
        }

        public Flag(IGameWorld world, Point location, int height, int maxHeight) : base(world, location, new Point(32, 32))
        {
            this.height = height;
            this.maxHeight = maxHeight;
            top = height == maxHeight;
            UpdateSprite();
        }
    }
}

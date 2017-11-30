namespace MelloMario.Objects.Blocks
{
    #region

    using System;
    using MelloMario.Factories;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Stair : BaseCollidableObject
    {
        public Stair(IWorld world, Point location, IListener<IGameObject> listener, bool isHidden = false) : base(
            world,
            location,
            listener,
            new Point(32, 32))
        {
            if (isHidden)
            {
                HideSprite();
            }
            else
            {
                UpdateSprite();
            }
        }

        protected override void OnUpdate(int time)
        {
        }

        protected override void OnCollision(
            IGameObject target,
            CollisionMode mode,
            CollisionMode modePassive,
            CornerMode corner,
            CornerMode cornerPassive)
        {
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateStairSprite());
        }
    }
}

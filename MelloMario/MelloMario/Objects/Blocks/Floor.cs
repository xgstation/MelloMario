namespace MelloMario.Objects.Blocks
{
    #region

    using System;
    using MelloMario.Factories;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    [Serializable]
    internal class Floor : BaseCollidableObject
    {
        public Floor(IWorld world, Point location, IListener<IGameObject> listener, bool isHidden = false) : base(
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

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateFloorSprite());
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

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
        }
    }
}

namespace MelloMario.Objects.Items
{
    #region

    using MelloMario.Factories;
    using MelloMario.Objects.Characters;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class FireBall : BasePhysicalObject
    {
        public FireBall(IGameWorld world, Point location, IListener<IGameObject> listener) : base(
            world,
            location,
            listener,
            new Point(16, 16),
            32)
        {
            ShowSprite(SpriteFactory.Instance.CreateFireSprite());
        }

        protected override void OnSimulation(int time)
        {
            base.OnSimulation(time);
        }

        protected override void OnUpdate(int time)
        {
            //throw new NotImplementedException();
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
            //throw new NotImplementedException();
        }

        protected override void OnCollision(
            IGameObject target,
            CollisionMode mode,
            CollisionMode modePassive,
            CornerMode corner,
            CornerMode cornerPassive)
        {
            if (target is Mario)
            {
            }
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
            //throw new NotImplementedException();
        }
    }
}

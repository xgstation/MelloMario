namespace MelloMario.Objects.Items
{
    #region

    using System;
    using MelloMario.Factories;
    using MelloMario.Objects.Characters;
    using Microsoft.Xna.Framework;

    #endregion

    [Serializable]
    internal class FireBall : BasePhysicalObject, ISoundable
    {
        public FireBall(IWorld world, Point location, IListener<IGameObject> listener, IListener<ISoundable> soundListener) : base(
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

        public event SoundHandler SoundEvent;
        public ISoundArgs SoundEventArgs { get; }
    }
}

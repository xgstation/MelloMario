﻿namespace MelloMario.Objects.Items
{
    #region

    using System;
    using MelloMario.Factories;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Items.FireFlowerStates;
    using MelloMario.Objects.Miscs;
    using MelloMario.Sounds.Effects;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class FireFlower : BaseCollidableObject, ISoundable
    {
        private bool collected;
        private IItemState state;

        public FireFlower(
            IWorld world,
            Point location,
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener,
            bool isUnveil = false) : base(
            world,
            location,
            listener,
            new Point(32, 32))
        {
            soundListener.Subscribe(this);
            SoundEventArgs = new SoundArgs();
            SoundEventArgs.SetMethodCalled();
            collected = false;
            if (isUnveil)
            {
                state = new Unveil(this);
                UpdateSprite();
            }
            else
            {
                state = new Normal(this);
                UpdateSprite();
            }
        }

        public IItemState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                UpdateSprite();
            }
        }

        public ISoundArgs SoundEventArgs { get; }

        public void Collect()
        {
            if (!collected)
            {
                ScorePoints(Const.SCORE_POWER_UP);
                World.Add(new PopingUpPoints(World, Boundary.Location, Const.SCORE_POWER_UP));
            }
            collected = true;
            RemoveSelf();
            //State.Collect();
        }

        public void UnveilMove(int delta)
        {
            Move(new Point(0, delta));
        }

        public event SoundHandler SoundEvent;

        protected override void OnUpdate(int time)
        {
            SoundEvent?.Invoke(this, SoundEventArgs);
            state.Update(time);
        }

        protected override void OnCollision(
            IGameObject target,
            CollisionMode mode,
            CollisionMode modePassive,
            CornerMode corner,
            CornerMode cornerPassive)
        {
            if (target is Mario && state is Normal)
            {
                Collect();
            }
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateFireFlowerSprite());
        }
    }
}

﻿namespace MelloMario.Objects.Enemies
{
    #region

    using System;
    using System.Linq;
    using MelloMario.Factories;
    using MelloMario.Objects.Enemies.KoopaStates;
    using MelloMario.Objects.Enemies.PiranhaStates;
    using MelloMario.Objects.Items;
    using MelloMario.Objects.Miscs;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Piranha : BasePhysicalObject
    {
        public Piranha(
            IWorld world,
            Point location,
            IListener<IGameObject> listener,
            Point size,
            int hiddenTime,
            int showTime,
            float pixelScale,
            string color = "Green") : base(world, location, listener, size, pixelScale)
        {
            HiddenTime = hiddenTime;
            ShowTime = showTime;
            State = new Hidden(this);
            ShowSprite(SpriteFactory.Instance.CreatePiranhaSprite(color));
        }

        public bool HasMarioAbove { get; private set; }

        public IPiranhaState State { get; set; }

        public int ShowTime { get; }

        public int HiddenTime { get; }

        public void Defeat()
        {
            ScorePoints(Const.SCORE_PIRANHA);
            World.Add(new PopingUpPoints(World, Boundary.Location, Const.SCORE_PIRANHA));
            State.Defeat();
            RemoveSelf();
        }

        protected override void OnSimulation(int time)
        {
            if (State is MovingUp)
            {
                SetVerticalVelocity(-Const.VELOCITY_PIRANHA);
            }
            else if (State is MovingDown)
            {
                SetVerticalVelocity(Const.VELOCITY_PIRANHA);
            }

            base.OnSimulation(time);
        }

        protected override void OnUpdate(int time)
        {
            State.Update(time);
            HasMarioAbove = DetectMario();
        }

        protected override void OnCollision(
            IGameObject target,
            CollisionMode mode,
            CollisionMode modePassive,
            CornerMode corner,
            CornerMode cornerPassive)
        {
            if (target is FireBall)
            {
                Defeat();
            }
            if (target is Koopa koopa)
            {
                if (koopa.State is MovingShell || koopa.State is NewlyMovingShell)
                {
                    Defeat();
                }
            }
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
            //throw new NotImplementedException();
        }

        private bool DetectMario()
        {
            return (from obj in World.ScanNearby(new Rectangle(Boundary.Center.X - 4, Boundary.Y, Boundary.Height, 0))
                where obj is ICharacter
                select obj).Any();
        }
    }
}

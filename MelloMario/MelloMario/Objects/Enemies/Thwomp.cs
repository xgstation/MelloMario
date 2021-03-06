﻿namespace MelloMario.Objects.Enemies
{
    #region

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using MelloMario.Factories;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Blocks.BrickStates;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Characters.ProtectionStates;
    using MelloMario.Objects.Enemies.ThwompStates;
    using MelloMario.Objects.Miscs;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using MelloMario.Sounds.Effects;

    #endregion

    internal class Thwomp : BasePhysicalObject
    {
        private bool onFloor = true;

        public ISoundArgs SoundEventArgs { get; }

        public Thwomp(IWorld world, Point location, IListener<IGameObject> listener) : base(
            world,
            location,
            listener,
            new Point(32, 32),
            32)
        {
            State = new ThwompStates.Normal(this);
            SoundEventArgs = new SoundArgs();
            NormalTime = 100;
            UpdateSprite();
        }

        public IState State { get; set; }

        public bool HasMarioBelow { get; private set; }

        public int NormalTime { get; }

        public void Defeat()
        {
            ScorePoints(Const.SCORE_THWOMP);
            World.Add(new PopingUpPoints(World, Boundary.Location, Const.SCORE_THWOMP));
            RemoveSelf();
        }

        protected override void OnUpdate(int time)
        {
            State.Update(time);
            HasMarioBelow = DetectMario();
        }

        protected override void OnSimulation(int time)
        {
            if (onFloor)
            {
                SetVerticalVelocity(-Const.VELOCITY_RISING_THWOMP);
            }
            else if (!onFloor && DetectMario())
            {
                ApplyGravity();
                SoundEventArgs.SetMethodCalled();
            }
            else
            {
                // Do nothing
            }
            base.OnSimulation(time);
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override void OnCollision(
            IGameObject target,
            CollisionMode mode,
            CollisionMode modePassive,
            CornerMode corner,
            CornerMode cornerPassive)
        {
            switch (target.GetType().Name)
            {
                case "MarioCharacter":
                    Mario mario = (Mario) target;
                    if (mario.ProtectionState is Starred)
                    {
                        Defeat();
                    }
                    break;
                case "Brick":
                    if (((Brick) target).State is Hidden)
                    {
                        break;
                    }
                    goto case "Stair";
                case "Question":
                    if (((Question) target).State is Blocks.QuestionStates.Hidden)
                    {
                        break;
                    }
                    goto case "Stair";
                case "Floor":
                    onFloor = true;
                    break;
                case "Pipeline":
                case "Stair":
                    if (mode == CollisionMode.Left)
                    {
                        Bounce(mode, new Vector2(), 1);
                        ChangeFacing(FacingMode.right);
                    }
                    else if (mode == CollisionMode.Right)
                    {
                        Bounce(mode, new Vector2(), 1);
                        ChangeFacing(FacingMode.left);
                    }
                    else if (mode == CollisionMode.Bottom)
                    {
                        Bounce(mode, new Vector2());
                    }
                    break;
            }
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
            Bounce(mode, new Vector2());
            if (mode == CollisionMode.InnerTop)
            {
                onFloor = false;
            }
        }

        private bool DetectMario()
        {
            return (from obj in World.ScanNearby(new Rectangle(Boundary.Center.X - 4, Boundary.Y, Boundary.Height, 500))
                    where obj is ICharacter
                    select obj).Any();
        }

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateThwompSprite(state.GetType().Name));
        }

        private void ChangeFacing(FacingMode facing)
        {
            Facing = facing;

            // Notice: The effect should be the same as changing state
            UpdateSprite();
        }
    }
}

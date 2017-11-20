﻿namespace MelloMario.Objects.Enemies
{
    #region

    using System.Linq;
    using Factories;
    using Interfaces.Objects.States;
    using Items;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PiranhaStates;
    using Theming;
    using UserInterfaces;

    #endregion

    internal class Piranha : BasePhysicalObject
    {
        public Piranha(IGameWorld world, Point location, IListener listener, Point size, int hiddenTime, int showTime, float pixelScale, string color = "Green") : base(world, location, listener, size, pixelScale)
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

        private bool DetectMario()
        {
            return (from obj in World.ScanNearby(new Rectangle(Boundary.Center.X - 4, Boundary.Y, Boundary.Height, 0)) where obj is ICharacter select obj).Any();
        }

        protected override void OnSimulation(int time)
        {
            switch (State.GetType().Name)
            {
                case "MovingUp":
                    Move(new Point(0, -1));
                    break;
                case "MovingDown":
                    Move(new Point(0, 1));
                    break;
                case "Hidden":
                    break;
                case "Show":
                    break;
                case "Defeated":
                    break;
                default:
                    break;
            }
            base.OnSimulation(time);
        }

        protected override void OnUpdate(int time)
        {
            State.Update(time);
            HasMarioAbove = DetectMario();
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch) { }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CollisionMode modePassive, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is FireBall)
            {
                Defeat();
            }
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode, CollisionMode modePassive)
        {
            //throw new NotImplementedException();
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
            //throw new NotImplementedException();
        }

        public void Defeat()
        {
            ScorePoints(Const.SCORE_PIRANHA);
            new PopingUpPoints(World, Boundary.Location, Const.SCORE_PIRANHA);
            State.Defeat();
            RemoveSelf();
        }
    }
}

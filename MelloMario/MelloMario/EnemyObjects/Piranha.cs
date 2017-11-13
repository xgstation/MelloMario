using System;
using MelloMario.EnemyObjects.PiranhaStates;
using MelloMario.Interfaces.Objects.States;
using Microsoft.Xna.Framework;
using MelloMario.Theming;

namespace MelloMario.EnemyObjects
{
    class Piranha : BasePhysicalObject
    {
        private int hiddenTime;
        private int showTime;
        private IPiranhaState state;

        public Piranha(IGameWorld world, Point location, Listener listener, Point size, int hiddenTime, int showTime, float pixelScale, string color = "Green") : base(world, location, listener, size, pixelScale)
        {
            this.hiddenTime = hiddenTime;
            this.showTime = showTime;
            state = new Hidden(this);
            ShowSprite(Factories.SpriteFactory.Instance.CreatePiranhaSprite(color));
        }

        public IPiranhaState State
        {
            set { state = value; }
        }

        public int ShowTime
        {
            get { return showTime; }
        }

        public int HiddenTime
        {
            get { return hiddenTime; }
        }
        protected override void OnSimulation(int time)
        {
            switch (state.GetType().Name)
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
            state.Update(time);
            //throw new NotImplementedException();
        }

        protected override void OnDraw(int time, Rectangle viewport)
        {
            //throw new NotImplementedException();
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            //throw new NotImplementedException();
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode)
        {
            //throw new NotImplementedException();
        }

        protected override void OnCollideWorld(CollisionMode mode)
        {
            //throw new NotImplementedException();
        }
    }
}

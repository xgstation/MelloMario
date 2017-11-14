using MelloMario.EnemyObjects.PiranhaStates;
using MelloMario.Interfaces.Objects.States;
using MelloMario.MarioObjects;
using Microsoft.Xna.Framework;
using MelloMario.Theming;

namespace MelloMario.EnemyObjects
{
    class Piranha : BasePhysicalObject
    {
        private int hiddenTime;
        private int showTime;
        private IPiranhaState state;
        public bool HasMarioAbove { get; private set; }
        public Piranha(IGameWorld world, Point location, Listener listener, Point size, int hiddenTime, int showTime, float pixelScale, string color = "Green") : base(world, location, listener, size, pixelScale)
        {
            this.hiddenTime = hiddenTime;
            this.showTime = showTime;
            state = new Hidden(this);
            ShowSprite(Factories.SpriteFactory.Instance.CreatePiranhaSprite(color));
        }

        private bool DetectMario()
        {
            foreach (var gameObject in World.ScanNearby(Boundary))
            {
                if (gameObject is Mario m)
                {
                    return m.Boundary.Intersects(new Rectangle(Boundary.X - 6, Boundary.Y - 4, Boundary.Width + 12,
                        Boundary.Height));
                }
            }
            return false;
        }
        public IPiranhaState State
        {
            get { return state; }
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
            HasMarioAbove = DetectMario();
        }

        protected override void OnDraw(int time, Rectangle viewport)
        {
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is Fire)
            {
                Defeat();
            }
        }

        protected override void OnCollideViewport(IPlayer player, CollisionMode mode)
        {
            //throw new NotImplementedException();
        }

        protected override void OnCollideWorld(CollisionMode mode)
        {
            //throw new NotImplementedException();
        }

        public void Defeat()
        {
            ScorePoints(GameConst.SCORE_KOOPA);
            state.Defeat();
            RemoveSelf();
        }
    }
}

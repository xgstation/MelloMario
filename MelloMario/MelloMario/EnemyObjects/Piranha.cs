using System.Linq;
using MelloMario.EnemyObjects.PiranhaStates;
using MelloMario.Interfaces.Objects.States;
using MelloMario.MarioObjects;
using MelloMario.UIObjects;
using Microsoft.Xna.Framework;
using MelloMario.Theming;
using MelloMario.ItemObjects;
using Microsoft.Xna.Framework.Graphics;

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
            return (from obj in World.ScanNearby(new Rectangle(Boundary.Center.X - 4, Boundary.Y, Boundary.Height, 0))
                    where obj is ICharacter
                    select obj).Any();

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

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
        }

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
            ScorePoints(GameConst.SCORE_PIRANHA);
            new PopingUpPoints(World, Boundary.Location, GameConst.SCORE_PIRANHA);
            state.Defeat();
            RemoveSelf();
        }
    }
}

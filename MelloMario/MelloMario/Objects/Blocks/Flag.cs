namespace MelloMario.Objects.Blocks
{
    #region

    using System;
    using MelloMario.Factories;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Miscs;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Flag : BaseCollidableObject
    {
        public delegate void TimeScoreHandler(Flag m, EventArgs e);

        private readonly int height;
        private readonly int maxHeight;
        private readonly bool top;

        private EventArgs eventInfo;

        public Flag(IWorld world, Point location, IListener<IGameObject> listener, int height, int maxHeight) :
            base(world, location, listener, new Point(32, 32))
        {
            listener.Subscribe(this);
            this.height = height;
            this.maxHeight = maxHeight;
            top = height == maxHeight;
            UpdateSprite();
        }

        public event TimeScoreHandler HandlerTimeScore;

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
            if (!(target is MarioCharacter mario))
            {
                return;
            }
            if (!mario.Active)
            {
                return;
            }
            if (top)
            {
                ChangeLives();
            }
            HandlerTimeScore?.Invoke(this, EventArgs.Empty);
            ScorePoints(Const.SCORE_FLAG_MAX * height / maxHeight);
            World.Add(new PopingUpPoints(World, Boundary.Location, Const.SCORE_FLAG_MAX * height / maxHeight));
            mario.FlagPole();
        }

        protected override void OnCollideWorld(CollisionMode mode, CollisionMode modePassive)
        {
        }

        private void UpdateSprite()
        {
            ShowSprite(SpriteFactory.Instance.CreateFlagSprite(top));
        }
    }
}

using Microsoft.Xna.Framework;

namespace MelloMario
{
    abstract class BaseTimedState<T> : BaseState<T>
    {
        private int elapsed;
        private int interval;

        protected abstract void OnTimer(GameTime time);

        public BaseTimedState(T owner, int totalTime) : base(owner)
        {
            this.interval = totalTime;
        }

        public override void Update(GameTime time)
        {
            // Note: if we will support recording/replaying, use a constant number here
            elapsed += time.ElapsedGameTime.Milliseconds;

            if (elapsed >= interval)
            {
                OnTimer(time);
                elapsed -= interval;
            }
        }
    }
}

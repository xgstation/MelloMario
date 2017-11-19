namespace MelloMario
{
    internal abstract class BaseTimedState<T> : BaseState<T>
    {
        private readonly int interval;
        private int elapsed;

        public BaseTimedState(T owner, int interval) : base(owner)
        {
            this.interval = interval;
        }

        protected float Progress
        {
            get { return (float) elapsed / interval; }
        }

        protected abstract void OnTimer(int time);

        public override void Update(int time)
        {
            // Note: if we will support recording/replaying, use a constant number here
            elapsed += time;

            if (elapsed >= interval)
            {
                OnTimer(time);
                elapsed -= interval;
            }
        }
    }
}
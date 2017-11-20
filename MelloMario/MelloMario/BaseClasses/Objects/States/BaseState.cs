namespace MelloMario
{
    internal abstract class BaseState<T> : IState
    {
        protected T Owner;

        public BaseState(T owner)
        {
            Owner = owner;
        }

        public abstract void Update(int time);
    }
}

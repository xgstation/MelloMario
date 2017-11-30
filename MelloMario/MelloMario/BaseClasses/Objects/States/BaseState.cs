namespace MelloMario.Objects
{
    #region

    using System;

    #endregion

    internal abstract class BaseState<T> : IState
    {
        protected T Owner;

        protected BaseState(T owner)
        {
            Owner = owner;
        }

        public abstract void Update(int time);
    }
}

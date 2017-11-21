namespace MelloMario.Controls.Commands
{
    #region

    using System;

    #endregion

    [Serializable]
    internal abstract class BaseCommand<T> : ICommand
    {
        protected T Receiver;

        protected BaseCommand(T receiver)
        {
            Receiver = receiver;
        }

        public abstract void Execute();
    }
}

namespace MelloMario.Commands
{
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

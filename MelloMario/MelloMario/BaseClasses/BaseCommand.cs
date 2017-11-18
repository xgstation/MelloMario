namespace MelloMario.Commands
{
    internal abstract class BaseCommand<T> : ICommand
    {
        protected T Receiver;

        public BaseCommand(T receiver)
        {
            Receiver = receiver;
        }

        public abstract void Execute();
    }
}
namespace MelloMario.Commands
{
    abstract class BaseCommand<T> : ICommand
    {
        protected T Receiver;

        public BaseCommand(T receiver)
        {
            this.Receiver = receiver;
        }

        public abstract void Execute();
    }
}
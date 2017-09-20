using MelloMario.Commands;

namespace MelloMario
{
    internal class DeadStateCommand : ICommand
    {
        private Script script;

        public DeadStateCommand(Script script)
        {
            this.script = script;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
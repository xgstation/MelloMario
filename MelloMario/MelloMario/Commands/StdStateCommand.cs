using MelloMario.Commands;

namespace MelloMario
{
    internal class StdStateCommand : ICommand
    {
        private Script script;

        public StdStateCommand(Script script)
        {
            this.script = script;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
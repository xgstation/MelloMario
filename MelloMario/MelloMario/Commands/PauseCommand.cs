using MelloMario.Commands;

namespace MelloMario
{
    internal class PauseCommand : ICommand
    {
        private Script script;

        public PauseCommand(Script script)
        {
            this.script = script;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
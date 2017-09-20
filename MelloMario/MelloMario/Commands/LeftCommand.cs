using MelloMario.Commands;

namespace MelloMario
{
    internal class LeftCommand : ICommand
    {
        private Script script;

        public LeftCommand(Script script)
        {
            this.script = script;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
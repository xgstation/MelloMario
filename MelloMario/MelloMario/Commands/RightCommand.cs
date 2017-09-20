using MelloMario.Commands;

namespace MelloMario
{
    internal class RightCommand : ICommand
    {
        private Script script;

        public RightCommand(Script script)
        {
            this.script = script;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
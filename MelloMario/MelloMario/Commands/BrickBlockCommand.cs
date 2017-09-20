using MelloMario.Commands;

namespace MelloMario
{
    internal class BrickBlockCommand : ICommand
    {
        private Script script;

        public BrickBlockCommand(Script script)
        {
            this.script = script;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
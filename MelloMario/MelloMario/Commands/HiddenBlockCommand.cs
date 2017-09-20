using MelloMario.Commands;

namespace MelloMario
{
    internal class HiddenBlockCommand : ICommand
    {
        private Script script;

        public HiddenBlockCommand(Script script)
        {
            this.script = script;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
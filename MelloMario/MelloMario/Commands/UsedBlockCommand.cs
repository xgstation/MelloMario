using MelloMario.Commands;

namespace MelloMario
{
    internal class UsedBlockCommand : ICommand
    {
        private Script script;

        public UsedBlockCommand(Script script)
        {
            this.script = script;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
using MelloMario.Commands;

namespace MelloMario
{
    internal class FireStateCommand : ICommand
    {
        private Script script;

        public FireStateCommand(Script script)
        {
            this.script = script;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
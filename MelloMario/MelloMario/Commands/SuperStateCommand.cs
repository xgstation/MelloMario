using MelloMario.Commands;

namespace MelloMario
{
    internal class SuperStateCommand : ICommand
    {
        private Script script;

        public SuperStateCommand(Script script)
        {
            this.script = script;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
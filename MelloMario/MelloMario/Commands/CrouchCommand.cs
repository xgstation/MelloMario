using MelloMario.Commands;

namespace MelloMario
{
    internal class CrouchCommand : ICommand
    {
        private Script script;

        public CrouchCommand(Script script)
        {
            this.script = script;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
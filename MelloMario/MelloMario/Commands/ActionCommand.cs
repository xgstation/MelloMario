using MelloMario.Commands;

namespace MelloMario
{
    internal class ActionCommand : ICommand
    {
        private Script script;

        public ActionCommand(Script script)
        {
            this.script = script;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
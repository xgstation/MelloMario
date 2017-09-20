using MelloMario.Commands;
using System.Diagnostics;

namespace MelloMario
{
    internal class JumpCommand : ICommand
    {
        private Script script;

        public JumpCommand(Script script)
        {
            this.script = script;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
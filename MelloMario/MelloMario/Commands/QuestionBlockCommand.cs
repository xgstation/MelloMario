using MelloMario.Commands;

namespace MelloMario
{
    internal class QuestionBlockCommand : ICommand
    {
        private Script script;

        public QuestionBlockCommand(Script script)
        {
            this.script = script;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
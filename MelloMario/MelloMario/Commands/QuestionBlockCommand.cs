using MelloMario.Commands;

namespace MelloMario
{
    internal class QuestionBlockCommand : ICommand
    {
        private GameModel model;

        public QuestionBlockCommand(GameModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
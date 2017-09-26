using System.Collections.Generic;

namespace MelloMario.Commands
{
    class QuestionBlockCommand : BaseCommand<List<IGameObject>>
    {
        public QuestionBlockCommand(List<IGameObject> obj) : base(obj)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
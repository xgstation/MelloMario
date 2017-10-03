using System.Collections.Generic;

namespace MelloMario.Commands
{
    class UsedBlockCommand : BaseCommand<List<IGameObject>>
    {
        public UsedBlockCommand(List<IGameObject> obj): base(obj)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
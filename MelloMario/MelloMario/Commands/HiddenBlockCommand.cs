using System.Collections.Generic;

namespace MelloMario.Commands
{
    class HiddenBlockCommand : BaseCommand<List<IGameObject>>
    {
        public HiddenBlockCommand(List<IGameObject> obj) : base(obj)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
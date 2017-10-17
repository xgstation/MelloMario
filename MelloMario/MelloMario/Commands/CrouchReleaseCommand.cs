using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Commands
{
    class CrouchReleaseCommand : BaseCommand<IGameCharacter>
    {
        public CrouchReleaseCommand(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.CrouchRelease();
        }
    }
}

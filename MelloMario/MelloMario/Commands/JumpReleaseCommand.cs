using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Commands
{
    class JumpReleaseCommand : BaseCommand<IGameCharacter>
    {
        public JumpReleaseCommand(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.JumpRelease();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Commands
{
    class ToggleFullScreen : BaseCommand<IGameModel>
    {
        public ToggleFullScreen(IGameModel receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ToggleFullScreen();
        }
    }
}

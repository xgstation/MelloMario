using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Controls.Commands
{
    internal class CursorDown : BaseCommand<Game1>
    {
        public CursorDown(Game1 receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.CursorDown();
        }
    }
}

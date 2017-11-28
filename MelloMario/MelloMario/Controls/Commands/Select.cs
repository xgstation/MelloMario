using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Controls.Commands
{
    internal class Select : BaseCommand<Game1>
    {
        public Select(Game1 receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.Select();
        }
    }
}

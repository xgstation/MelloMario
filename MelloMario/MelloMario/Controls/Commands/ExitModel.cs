using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Controls.Commands
{
    internal class ExitModel : BaseCommand<IModel>
    {
        public ExitModel (IModel model) : base(model)
        {
        }

        public override void Execute()
        {
            Receiver.Exit();
        }
    }

}

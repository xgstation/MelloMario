using MelloMario.MarioObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Commands
{
    class FallCommand : BaseCommand<Mario>
    {
        public FallCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            //Receiver.userInY = Mario.H_MAX_ACCEL;
            Receiver.Jump();
        }
    }
}

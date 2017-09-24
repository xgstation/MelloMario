using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario
{
    class CommandFactory : ICommandFactory
    {
        ICommand createGameModelCommand(string actionName, GameModel model)
        {
            // TODO: implement it
        }
        ICommand createMarioCommand(string actionName, Mario mario)
        {
            // TODO: implement it
        }
    }
}

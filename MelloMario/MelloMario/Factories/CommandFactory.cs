using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelloMario.MarioObjects;

namespace MelloMario
{
    class CommandFactory : ICommandFactory
    {
        public ICommand CreateGameModelCommand(string actionName, GameModel model)
        {
            throw new NotImplementedException();
        }
        
        public ICommand CreateMarioCommand(string actionName, Mario mario)
        {
            throw new NotImplementedException();
        }
    }
}

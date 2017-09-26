using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario
{
    interface ICommandFactory
    {
        ICommand CreateGameControlCommand(string action, GameModel model);
        ICommand CreateMiscCommand(string action, List<IGameObject> gameObject);
        ICommand CreateMarioCommand(string action, MarioObjects.Mario mario);
    }
}

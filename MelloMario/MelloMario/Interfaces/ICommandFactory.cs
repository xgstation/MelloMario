using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario
{
    interface ICommandFactory
    {
        ICommand CreateGameModelCommand(string action, GameModel model);
        ICommand CreateGameObjectCommand(string action, IGameObject gameObject);
        ICommand CreateMarioCommand(string action, MarioObjects.Mario mario);
    }
}

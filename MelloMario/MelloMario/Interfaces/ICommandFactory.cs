using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario
{
    interface ICommandFactory
    {
        ICommand CreateGameModelCommand(string actionName, GameModel model);
        ICommand CreateGameObjectCommand(string actionName, IGameObject gameObject);
        ICommand CreateMarioCommand(string actionName, MarioObjects.Mario mario);
    }
}

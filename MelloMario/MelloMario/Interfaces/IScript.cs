using System.Collections.Generic;

namespace MelloMario
{
    internal interface IScript
    {
        void Bind(IEnumerable<IController> controllers, IGameModel model, ICharacter character);
    }
}
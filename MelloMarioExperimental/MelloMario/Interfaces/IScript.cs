using System.Collections.Generic;

namespace MelloMario
{
    interface IScript
    {
        void Bind(IEnumerable<IController> controllers, IGameModel model, ICharacter character);
    }
}

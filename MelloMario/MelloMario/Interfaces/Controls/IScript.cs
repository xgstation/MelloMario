using System.Collections.Generic;

namespace MelloMario
{
    internal interface IScript
    {
        void Bind(IEnumerable<IController> controllers, IModel model, ICharacter character);
    }
}

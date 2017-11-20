namespace MelloMario
{
    #region

    using System.Collections.Generic;

    #endregion

    internal interface IScript
    {
        void Bind(IEnumerable<IController> controllers, IModel model, ICharacter character);
    }
}

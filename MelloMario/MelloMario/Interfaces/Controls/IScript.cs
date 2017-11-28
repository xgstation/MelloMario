namespace MelloMario
{
    #region

    using System.Collections.Generic;

    #endregion

    internal interface IScript<in T>
    {
        void Bind(IEnumerable<IController> controllers, T objectToBeBind);
    }
}

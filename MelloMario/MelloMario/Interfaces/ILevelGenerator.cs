namespace MelloMario
{
    #region

    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    #endregion

    internal interface ILevelGenerator
    {
        IEnumerable<IGameObject> Request(Rectangle range);
    }
}

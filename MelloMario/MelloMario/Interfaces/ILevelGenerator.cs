namespace MelloMario
{
    #region

    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    #endregion

    internal interface ILevelGenerator
    {
        void Request(Rectangle range, IWorld world);
    }
}

namespace MelloMario
{
    #region

    using Microsoft.Xna.Framework;

    #endregion

    internal interface ILevelGenerator
    {
        void Request(IWorld world, Rectangle range);
    }
}

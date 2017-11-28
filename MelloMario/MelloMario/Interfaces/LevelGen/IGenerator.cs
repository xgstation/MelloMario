namespace MelloMario
{
    #region

    using Microsoft.Xna.Framework;

    #endregion

    internal interface IGenerator
    {
        void Request(IWorld world, Rectangle range);
    }
}

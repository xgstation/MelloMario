namespace MelloMario
{
    #region

    using Microsoft.Xna.Framework;

    #endregion

    internal interface IGameObject : IObject
    {
        Rectangle Boundary { get; }
    }
}

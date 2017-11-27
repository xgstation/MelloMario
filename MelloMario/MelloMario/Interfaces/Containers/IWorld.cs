namespace MelloMario
{
    #region

    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    #endregion

    internal enum WorldType
    {
        normal,

        underground
        // underwater, bowsercastle, ...
    }

    internal interface IWorld : IContainer<IGameObject>
    {
        string ID { get; }
        WorldType Type { get; }
        Rectangle Boundary { get; }
        void Extend(int left, int right, int top, int bottom);

        IEnumerable<IGameObject> ScanNearby(Rectangle range, bool allowExtension = false);
        Point GetRespawnPoint(Point location);
    }
}

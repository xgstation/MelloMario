namespace MelloMario
{
    #region

    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    #endregion

    internal enum WorldType
    {
        normal,
        underground,
        // underwater, bowsercastle, ...
    }

    internal interface IGameWorld : IContainer<IGameObject>
    {
        string ID { get; }
        WorldType Type { get; }
        Rectangle Boundary { get; }
        void Extend(int x, int y);

        IEnumerable<IGameObject> ScanNearby(Rectangle range);
        Point GetInitialPoint();
        Point GetRespawnPoint(Point location);
    }
}

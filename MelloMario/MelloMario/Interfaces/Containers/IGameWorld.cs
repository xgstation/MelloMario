﻿namespace MelloMario
{
    #region

    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    #endregion

    internal interface IGameWorld : IContainer<IGameObject>
    {
        string ID { get; }
        string Type { get; }
        Rectangle Boundary { get; }
        void Extend(int x, int y);

        IEnumerable<IGameObject> ScanNearby(Rectangle range);
        Point GetInitialPoint();
        Point GetRespawnPoint(Point location);
    }
}

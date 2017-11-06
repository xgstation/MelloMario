using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IGameWorld : IContainer<IGameObject>, IDisposable
    {
        Rectangle Boundary { get; }
        Point InitialSpawnPoint { get; set; }

        IEnumerable<IGameObject> ScanNearby(Rectangle range);
        void AddRespawnPoint(Point newPoint);
        Point GetRespawnPoint(Point location);
    }
}

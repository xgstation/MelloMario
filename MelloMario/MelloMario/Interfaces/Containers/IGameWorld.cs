using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IGameWorld : IContainer<IGameObject>
    {
        Rectangle Boundary { get; }

        IEnumerable<IGameObject> ScanNearby(Rectangle range);
        Point GetInitialPoint();
        Point GetRespawnPoint(Point location);
    }
}

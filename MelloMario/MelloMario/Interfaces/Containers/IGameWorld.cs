using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IGameWorld : IContainer<IGameObject>
    {
        string Id { get; }
        Rectangle Boundary { get; }
        bool FlagIsTouched { get; set; }

        IEnumerable<IGameObject> ScanNearby(Rectangle range);
        Point GetInitialPoint();
        Point GetRespawnPoint(Point location);
    }
}

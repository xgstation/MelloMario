using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IGameWorld
    {
        string Id { get; }
        Rectangle Boundary { get; }

        IEnumerable<IGameObject> ScanNearby(Rectangle range);
        Point GetInitialPoint();
        Point GetRespawnPoint(Point location);
        void Add(IGameObject value);
        void Move(IGameObject value);
        void Remove(IGameObject value);
        void Update();
    }
}

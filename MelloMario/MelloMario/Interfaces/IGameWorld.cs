using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IGameWorld
    {
        Rectangle Boundary { get; }
        GameModel Model { get; }
        IEnumerable<IGameObject> ScanObjects();
        IEnumerable<IGameObject> ScanNearbyObjects(IGameObject gameObject);
        void AddObject(IGameObject gameObject);
        void MoveObject(IGameObject gameObject);
        void RemoveObject(IGameObject gameObject);
        void Update();
    }
}

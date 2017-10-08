using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface IGameObjectManager
    {
        Rectangle Boundary { get; }

        IEnumerable<IGameObject> ScanObjects();
        IEnumerable<IGameObject> ScanNearbyObjects(IGameObject gameObject);
        void AddObject(IGameObject gameObject);
        void RemoveObject(IGameObject gameObject);
    }
}

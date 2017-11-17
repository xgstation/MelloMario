using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IGameObject : IObject
    {
        Rectangle Boundary { get; }
    }
}

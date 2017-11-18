using Microsoft.Xna.Framework;

namespace MelloMario
{
    internal interface IGameObject : IObject
    {
        Rectangle Boundary { get; }
    }
}
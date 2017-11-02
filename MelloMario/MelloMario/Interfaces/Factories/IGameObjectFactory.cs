using Microsoft.Xna.Framework;
using System;

namespace MelloMario
{
    interface IGameObjectFactory
    {
        Tuple<IGameControl, IGameObject> CreateGameCharacter(string type, IGameWorld world, Point location);
        IGameObject CreateGameObject(string type, IGameWorld world, Point location);
    }
}

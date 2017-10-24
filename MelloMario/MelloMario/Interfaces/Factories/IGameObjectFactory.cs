using Microsoft.Xna.Framework;
using System;

namespace MelloMario
{
    interface IGameObjectFactory
    {
        IGameWorld CreateGameWorld(Point size);
        Tuple<IGameCharacter, IGameObject> CreateGameCharacter(string type, IGameWorld world, Point location);
        IGameObject CreateGameObject(string type, IGameWorld world, Point location);
    }
}

using MelloMario.Theming;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MelloMario
{
    interface IGameObjectFactory
    {
        Tuple<ICharacter, IGameObject> CreateGameCharacter(string type, IGameWorld world, Point location, Listener listener);
        IGameObject CreateGameObject(string type, IGameWorld world, Point location, Listener listener);
        IEnumerable<IGameObject> CreateGameObjectGroup(string type, IGameWorld world, Point location, int count, Listener listener);
    }
}

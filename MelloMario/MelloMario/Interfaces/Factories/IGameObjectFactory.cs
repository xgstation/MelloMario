using MelloMario.Theming;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MelloMario
{
    interface IGameObjectFactory
    {
        ICharacter CreateGameCharacter(string type, IGameWorld world, IPlayer player, Point location, Listener listener);
        IGameObject CreateGameObject(string type, IGameWorld world, Point location, Listener listener);
        IEnumerable<IGameObject> CreateGameObjectGroup(string type, IGameWorld world, Point location, int count, Listener listener);
    }
}

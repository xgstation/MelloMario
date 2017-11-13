using MelloMario.Theming;
using Microsoft.Xna.Framework;
using System;

namespace MelloMario
{
    interface IGameObjectFactory
    {
        Tuple<ICharacter, IGameObject> CreateGameCharacter(string type, IGameSession session, IGameWorld world, Point location, Listener listener);
        IGameObject CreateGameObject(string type, IGameWorld world, Point location, Listener listener);
        IGameObject[] CreateFlagPole(IGameWorld world, Point location, Listener listener, int height);
    }
}

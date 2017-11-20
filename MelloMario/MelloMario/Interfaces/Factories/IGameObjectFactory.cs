namespace MelloMario
{
    #region

    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    #endregion

    internal interface IGameObjectFactory
    {
        ICharacter CreateGameCharacter(
            string type,
            IGameWorld world,
            IPlayer player,
            Point location,
            IListener listener);

        IGameObject CreateGameObject(string type, IGameWorld world, Point location, IListener listener);

        IEnumerable<IGameObject> CreateGameObjectGroup(
            string type,
            IGameWorld world,
            Point location,
            int count,
            IListener listener);
    }
}

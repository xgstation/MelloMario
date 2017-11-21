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
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener);

        IGameObject CreateGameObject(
            string type,
            IGameWorld world,
            Point location,
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener);

        IEnumerable<IGameObject> CreateGameObjectGroup(
            string type,
            IGameWorld world,
            Point location,
            int count,
            IListener<IGameObject> listener);
    }
}

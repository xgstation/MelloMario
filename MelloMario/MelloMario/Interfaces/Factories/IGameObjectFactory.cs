namespace MelloMario
{
    using System;
    #region

    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    #endregion

    internal interface IGameObjectFactory
    {
        Tuple<IGameObject, ICharacter> CreateCharacter(
            string type,
            IWorld world,
            IPlayer player,
            Point location,
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener);

        ICamera CreateCamera();

        IGameObject CreateGameObject(
            string type,
            IWorld world,
            Point location,
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener);

        IEnumerable<IGameObject> CreateGameObjectGroup(
            string type,
            IWorld world,
            Point location,
            int count,
            IListener<IGameObject> listener);
    }
}

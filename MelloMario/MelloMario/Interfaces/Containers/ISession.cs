namespace MelloMario
{
    #region

    using System.Collections.Generic;

    #endregion

    internal interface ISession : IContainer<IPlayer>
    {
        IEnumerable<IWorld> ScanWorlds();
        IEnumerable<IPlayer> ScanPlayers();
    }
}

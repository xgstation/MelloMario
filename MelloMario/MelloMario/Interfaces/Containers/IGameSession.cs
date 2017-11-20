namespace MelloMario
{
    #region

    using System.Collections.Generic;

    #endregion

    internal interface IGameSession : IContainer<IPlayer>
    {
        IEnumerable<IGameWorld> ScanWorlds();
        IEnumerable<IPlayer> ScanPlayers();
    }
}

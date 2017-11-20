using System.Collections.Generic;

namespace MelloMario
{
    internal interface IGameSession : IContainer<IPlayer>
    {
        IEnumerable<IGameWorld> ScanWorlds();
        IEnumerable<IPlayer> ScanPlayers();
    }
}

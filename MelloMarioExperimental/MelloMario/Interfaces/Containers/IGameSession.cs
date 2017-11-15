using System.Collections.Generic;

namespace MelloMario
{
    interface IGameSession : IContainer<IPlayer>
    {
        IEnumerable<IGameWorld> ScanWorlds();
        IEnumerable<IPlayer> ScanPlayers();
    }
}

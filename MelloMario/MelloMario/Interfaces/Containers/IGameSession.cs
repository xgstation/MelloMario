using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IGameSession : IContainer<IPlayer>
    {
        IEnumerable<IGameWorld> ScanWorlds();
    }
}

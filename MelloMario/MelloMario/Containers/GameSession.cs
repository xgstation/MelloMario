using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MelloMario.Containers
{
    class GameSession : BaseContainer<IGameWorld, IPlayer>, IGameSession
    {
        protected override IGameWorld GetKey(IPlayer value)
        {
            return value.World;
        }

        public IEnumerable<IGameWorld> ScanWorlds()
        {
            return ScanKeys();
        }

        public IEnumerable<IPlayer> ScanPlayers()
        {
            return ScanValues();
        }
    }
}

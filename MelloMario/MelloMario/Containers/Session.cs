using System.Collections.Generic;

namespace MelloMario.Containers
{
    internal class Session : BaseContainer<IGameWorld, IPlayer>, IGameSession
    {
        public IEnumerable<IGameWorld> ScanWorlds()
        {
            return ScanKeys();
        }

        public IEnumerable<IPlayer> ScanPlayers()
        {
            return ScanValues();
        }

        protected override IGameWorld GetKey(IPlayer value)
        {
            return value.Character.CurrentWorld;
        }
    }
}

namespace MelloMario.Containers
{
    #region

    using System.Collections.Generic;

    #endregion

    internal class Session : BaseContainer<IWorld, IPlayer>, ISession
    {
        public IEnumerable<IWorld> ScanWorlds()
        {
            return ScanKeys();
        }

        public IEnumerable<IPlayer> ScanPlayers()
        {
            return ScanValues();
        }

        protected override IWorld GetKey(IPlayer value)
        {
            return value.Character.CurrentWorld;
        }
    }
}

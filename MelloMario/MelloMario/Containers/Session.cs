namespace MelloMario.Containers
{
    #region

    using System;
    using System.Collections.Generic;

    #endregion

    [Serializable]
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

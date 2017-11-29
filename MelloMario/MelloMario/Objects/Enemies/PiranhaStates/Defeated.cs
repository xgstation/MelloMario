namespace MelloMario.Objects.Enemies.PiranhaStates
{
    #region

    using System;

    #endregion

    [Serializable]
    internal class Defeated : BaseState<Piranha>, IPiranhaState
    {
        public Defeated(Piranha owner) : base(owner)
        {
        }

        public void Defeat()
        {
            //Do nothing
        }

        public override void Update(int time)
        {
        }
    }
}

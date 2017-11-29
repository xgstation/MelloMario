namespace MelloMario.Objects.Enemies.PiranhaStates
{
    #region

    using System;

    #endregion

    [Serializable]
    internal class Show : BaseTimedState<Piranha>, IPiranhaState
    {
        public Show(Piranha owner) : base(owner, owner.ShowTime)
        {
        }

        public void Defeat()
        {
            Owner.State = new Defeated(Owner);
        }

        protected override void OnTimer(int time)
        {
            Owner.State = new MovingDown(Owner);
        }
    }
}

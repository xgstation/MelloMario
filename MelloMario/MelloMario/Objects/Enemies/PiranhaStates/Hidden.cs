namespace MelloMario.Objects.Enemies.PiranhaStates
{
    #region

    using System;

    #endregion

    [Serializable]
    internal class Hidden : BaseTimedState<Piranha>, IPiranhaState
    {
        public Hidden(Piranha owner) : base(owner, owner.HiddenTime)
        {
        }

        public void Defeat()
        {
            //cannot be defeated at this state
        }

        protected override void OnTimer(int time)
        {
            if (!Owner.HasMarioAbove)
            {
                Owner.State = new MovingUp(Owner);
            }
        }
    }
}

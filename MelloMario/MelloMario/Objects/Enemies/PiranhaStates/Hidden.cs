namespace MelloMario.Objects.Enemies.PiranhaStates
{
    #region

    using MelloMario.Interfaces.Objects.States;

    #endregion

    internal class Hidden : BaseState<Piranha>, IPiranhaState
    {
        private int elapsed;

        public Hidden(Piranha owner) : base(owner)
        {
            elapsed = 0;
        }

        public override void Update(int time)
        {
            if (Owner.HasMarioAbove)
            {
                return;
            }
            elapsed += time;
            if (elapsed > Owner.HiddenTime)
            {
                Owner.State = new MovingUp(Owner);
            }
        }

        public void Defeat()
        {
            //cannot be defeated at this state
        }
    }
}

namespace MelloMario.Objects.Enemies.PiranhaStates
{
    #region

    using MelloMario.Interfaces.Objects.States;

    #endregion

    internal class Show : BaseState<Piranha>, IPiranhaState
    {
        private int elapsed;

        public Show(Piranha owner) : base(owner)
        {
            elapsed = 0;
        }

        public void Defeat()
        {
            Owner.State = new Defeated(Owner);
        }

        public override void Update(int time)
        {
            elapsed += time;
            if (elapsed > Owner.ShowTime)
            {
                Owner.State = new MovingDown(Owner);
            }
        }
    }
}

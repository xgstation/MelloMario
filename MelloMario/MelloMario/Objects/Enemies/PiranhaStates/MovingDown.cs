namespace MelloMario.Objects.Enemies.PiranhaStates
{
    #region

    using Interfaces.Objects.States;

    #endregion

    internal class MovingDown : BaseState<Piranha>, IPiranhaState
    {
        private readonly int initialY;

        public MovingDown(Piranha owner) : base(owner)
        {
            initialY = owner.Boundary.Y;
        }

        public override void Update(int time)
        {
            if (Owner.Boundary.Y >= initialY + 48)
            {
                Owner.State = new Hidden(Owner);
            }
        }

        public void Defeat()
        {
            Owner.State = new Defeated(Owner);
        }
    }
}

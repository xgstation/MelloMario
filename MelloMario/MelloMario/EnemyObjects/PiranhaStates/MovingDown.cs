using MelloMario.Interfaces.Objects.States;

namespace MelloMario.EnemyObjects.PiranhaStates
{
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
                Owner.State = new Hidden(Owner);
        }

        public void Defeat()
        {
            Owner.State = new Defeated(Owner);
        }
    }
}
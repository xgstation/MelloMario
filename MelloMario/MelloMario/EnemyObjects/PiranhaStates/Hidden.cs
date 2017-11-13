using MelloMario.Interfaces.Objects.States;

namespace MelloMario.EnemyObjects.PiranhaStates
{
    class Hidden : BaseState<Piranha>, IPiranhaState
    {
        private int elapsed;
        public override void Update(int time)
        {
            elapsed += time;
            if (elapsed > Owner.HiddenTime)
            {
                Owner.State = new MovingUp(Owner);
            }
        }

        public Hidden(Piranha owner) : base(owner)
        {
            elapsed = 0;
        }
    }
}

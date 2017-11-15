using MelloMario.Interfaces.Objects.States;
using MelloMario.Sounds;

namespace MelloMario.EnemyObjects.PiranhaStates
{
    class Defeated : BaseState<Piranha>, IPiranhaState
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

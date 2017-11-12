using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelloMario.Interfaces.Objects.States;

namespace MelloMario.EnemyObjects.PiranhaStates
{
    class Defeated: BaseState<Piranha>, IPiranhaState
    {
        public Defeated(Piranha owner) : base(owner)
        {
        }

        public override void Update(int time)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelloMario.Interfaces.Objects.States;

namespace MelloMario.EnemyObjects.PiranhaStates
{
    class Show : BaseState<Piranha>, IPiranhaState
    {
        private int elapsed;
        public Show(Piranha owner) : base(owner)
        {
            elapsed = 0;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelloMario.Interfaces.Objects.States;

namespace MelloMario.EnemyObjects.PiranhaStates
{
    class MovingDown : BaseState<Piranha>, IPiranhaState
    {
        private int initialY;
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
    }
}

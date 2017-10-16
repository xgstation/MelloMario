using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class Shell : BaseState<Koopa>, IKoopaState
    {
        public Shell(Koopa owner) : base(owner)
        {
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void JumpOn()
        {
        }

        public void Defeat()
        {
            Owner.State = new Defeated(Owner);
        }

        public override void Update(GameTime time)
        {
        }
    }
}

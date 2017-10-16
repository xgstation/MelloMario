using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class Defeated : BaseState<Koopa>, IKoopaState
    {
        public Defeated(Koopa owner) : base(owner)
        {
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void JumpOn()
        {
            Owner.State = new Shell(Owner);
        }

        public void Defeat()
        {
        }

        public override void Update(GameTime time)
        {
        }
    }
}

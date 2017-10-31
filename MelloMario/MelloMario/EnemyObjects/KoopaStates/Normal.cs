using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class Normal : BaseState<Koopa>, IKoopaState
    {
        public Normal(Koopa owner) : base(owner)
        {
        }

        public void Show()
        {

        }

        public void JumpOn()
        {
            Owner.State = new Defeated(Owner);
        }

        public void Defeat()
        {
            Owner.State = new Defeated(Owner);
        }

        public override void Update(GameTime time)
        {
        }

        public void Pushed()
        {
            //can't push normal koopa
        }
    }
}

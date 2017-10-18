using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class MovingShell : BaseState<Koopa>, IKoopaState
    {
        public MovingShell(Koopa owner) : base(owner)
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

        public void Pushed()
        {
            //do nothing for this sprint
        }
    }
}

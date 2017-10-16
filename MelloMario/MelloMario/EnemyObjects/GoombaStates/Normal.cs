using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.GoombaStates
{
    class Normal : BaseState<Goomba>, IGoombaState
    {
        public Normal(Goomba owner) : base(owner)
        {
        }

        public void Show()
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

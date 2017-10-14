using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.GoombaStates
{
    class Defeated : BaseState<Goomba>, IGoombaState
    {

        private Goomba enemyGoomba;

        public Defeated(Goomba goomba1)
        {
            enemyGoomba = goomba1;

        }

        public void Show()
        {
            enemyGoomba.State = new Normal(enemyGoomba);
        }

        public void Defeat()
        {

        }

        public override void Update(GameTime time)
        {

        }
    }
}

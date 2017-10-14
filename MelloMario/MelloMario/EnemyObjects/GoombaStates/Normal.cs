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
        private Goomba enemyGoomba;

        public Normal(Goomba goomba1)
        {
            enemyGoomba = goomba1;
        }

        public void Show()
        {

        }

        public void Defeat()
        {
            enemyGoomba.State = new Defeated(enemyGoomba);
        }

        public override void Update(GameTime time)
        {

        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class RedKoopaNormal : IKoopaState
    {
        private RedKoopa enemyRedKoopa;

        public RedKoopaNormal(RedKoopa koopaRed)
        {
            enemyRedKoopa = koopaRed;
        }
        public void Show()
        {

        }

        public void JumpOn()
        {
            enemyRedKoopa.State = new RedKoopaJumpOn(enemyRedKoopa);
        }

        public void Defeat()
        {
            enemyRedKoopa.State = new RedKoopaDefeated(enemyRedKoopa);
        }

        public void Update(GameTime time)
        {
        }
    }
}

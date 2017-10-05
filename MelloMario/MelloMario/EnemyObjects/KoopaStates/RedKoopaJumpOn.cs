using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class RedKoopaJumpOn : IKoopaState
    {
        private RedKoopa enemyRedKoopa;

        public RedKoopaJumpOn(RedKoopa koopaRed)
        {
            enemyRedKoopa = koopaRed;
        }

        public void Show()
        {
            enemyRedKoopa.State = new RedKoopaNormal(enemyRedKoopa);
        }

        public void JumpOn()
        {
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

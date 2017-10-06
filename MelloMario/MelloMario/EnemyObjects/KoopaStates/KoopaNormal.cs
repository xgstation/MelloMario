using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class KoopaNormal : IKoopaState
    {
        private Koopa enemyRedKoopa;

        public KoopaNormal(Koopa koopaRed)
        {
            enemyRedKoopa = koopaRed;
        }

        public void Show()
        {

        }

        public void JumpOn()
        {
            enemyRedKoopa.State = new KoopaJumpOn(enemyRedKoopa);
        }

        public void Defeat()
        {
            enemyRedKoopa.State = new KoopaDefeated(enemyRedKoopa);
        }

        public void Update(GameTime time)
        {
        }
    }
}

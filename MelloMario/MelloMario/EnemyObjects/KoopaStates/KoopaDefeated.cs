using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class KoopaDefeated : IKoopaState
    {
        private Koopa enemyRedKoopa;

        public KoopaDefeated(Koopa koopaRed)
        {
            enemyRedKoopa = koopaRed;
        }

        public void Show()
        {
            enemyRedKoopa.State = new KoopaNormal(enemyRedKoopa);
        }

        public void JumpOn()
        {
            enemyRedKoopa.State = new KoopaJumpOn(enemyRedKoopa);
        }

        public void Defeat()
        {
        }

        public void Update(GameTime time)
        {
        }
    }
}

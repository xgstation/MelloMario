using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class KoopaJumpOn : IKoopaState
    {
        private Koopa enemyRedKoopa;

        public KoopaJumpOn(Koopa koopaRed)
        {
            enemyRedKoopa = koopaRed;
        }

        public void Show()
        {
            enemyRedKoopa.State = new KoopaNormal(enemyRedKoopa);
        }

        public void JumpOn()
        {
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

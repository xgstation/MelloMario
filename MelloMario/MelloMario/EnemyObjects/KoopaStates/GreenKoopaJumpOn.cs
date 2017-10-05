using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class GreenKoopaJumpOn : IKoopaState
    {
        private GreenKoopa enemyGreenKoopa;

        public GreenKoopaJumpOn(GreenKoopa koopaGreen)
        {
            enemyGreenKoopa = koopaGreen;
        }

        public void Show()
        {
            enemyGreenKoopa.State = new GreenKoopaNormal(enemyGreenKoopa);
        }

        public void JumpOn()
        {
        }

        public void Defeat()
        {
            enemyGreenKoopa.State = new GreenKoopaDefeated(enemyGreenKoopa);
        }

        public void Update(GameTime time)
        {
        }
    }
}

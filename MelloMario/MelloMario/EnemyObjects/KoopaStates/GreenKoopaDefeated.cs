using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class GreenKoopaDefeated : IKoopaState
    {
        private GreenKoopa enemyGreenKoopa;

        public GreenKoopaDefeated(GreenKoopa koopaGreen)
        {
            enemyGreenKoopa = koopaGreen;
        }

        public void Show()
        {
            enemyGreenKoopa.State = new GreenKoopaNormal(enemyGreenKoopa);
        }

        public void JumpOn()
        {
            enemyGreenKoopa.State = new GreenKoopaJumpOn(enemyGreenKoopa);
        }

        public void Defeat()
        {
        }

        public void Update(GameTime time)
        {
        }
    }
}

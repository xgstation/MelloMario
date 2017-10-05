using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class GreenKoopaNormal : IKoopaState
    {
        private GreenKoopa enemyGreenKoopa;

        public GreenKoopaNormal(GreenKoopa koopaGreen)
        {
            enemyGreenKoopa = koopaGreen;
        }
        public void Show()
        {

        }

        public void JumpOn()
        {
            enemyGreenKoopa.State = new GreenKoopaJumpOn(enemyGreenKoopa);
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

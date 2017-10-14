using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class Normal : BaseState<Koopa>, IKoopaState
    {
        private Koopa enemyRedKoopa;

        public Normal(Koopa koopaRed)
        {
            enemyRedKoopa = koopaRed;
        }

        public void Show()
        {

        }

        public void JumpOn()
        {
            enemyRedKoopa.State = new Shell(enemyRedKoopa);
        }

        public void Defeat()
        {
            enemyRedKoopa.State = new Defeated(enemyRedKoopa);
        }

        public override void Update(GameTime time)
        {
        }
    }
}

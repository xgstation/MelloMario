using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class Defeated : BaseState<Koopa>, IKoopaState
    {
        public Defeated(Koopa koopaRed)
        {
            enemyRedKoopa = koopaRed;
        }

        public void Show()
        {
            enemyRedKoopa.State = new Normal(enemyRedKoopa);
        }

        public void JumpOn()
        {
            enemyRedKoopa.State = new Shell(enemyRedKoopa);
        }

        public void Defeat()
        {
        }

        public override void Update(GameTime time)
        {
        }
    }
}

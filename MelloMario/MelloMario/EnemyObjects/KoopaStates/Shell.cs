using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class Shell : BaseState<Koopa>, IKoopaState
    {
        public Shell(Koopa koopaRed)
        {
            enemyRedKoopa = koopaRed;
        }

        public void Show()
        {
            enemyRedKoopa.State = new Normal(enemyRedKoopa);
        }

        public void JumpOn()
        {
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

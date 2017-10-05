using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class RedKoopaDefeated : IKoopaState
    {
        private RedKoopa enemyRedKoopa;
        private ISprite redKoopa;
        public RedKoopaDefeated(RedKoopa koopaRed)
        {
            enemyRedKoopa = koopaRed;
            redKoopa = SpriteFactory.Instance.CreateRedKoopaSprite("Defeated");

        }
        public void Show()
        {
            enemyRedKoopa.State = new RedKoopaNormal(enemyRedKoopa);
        }

        public void JumpOn()
        {
            enemyRedKoopa.State = new RedKoopaJumpOn(enemyRedKoopa);
        }

        public void Defeat()
        {

        }

        public void Update(GameTime time)
        {
            redKoopa.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            redKoopa.Draw(spriteBatch, location);

        }
    }
}

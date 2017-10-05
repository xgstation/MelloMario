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
        public void ChangeToNormal()
        {
            enemyRedKoopa.State = new RedKoopaNormal(enemyRedKoopa);
        }

        public void ChangeToJumpOn()
        {
            enemyRedKoopa.State = new RedKoopaJumpOn(enemyRedKoopa);
        }

        public void ChangeToDefeated()
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

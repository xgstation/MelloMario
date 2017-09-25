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
        public void transNormal()
        {
            enemyRedKoopa.redKoopaState = new RedKoopaNormal(enemyRedKoopa);
        }

        public void transJumpOn()
        {
            enemyRedKoopa.redKoopaState = new RedKoopaJumpOn(enemyRedKoopa);
        }

        public void transDefeated()
        {

        }

        public void Update(GameTime gameTime)
        {
            redKoopa.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            redKoopa.Draw(spriteBatch, location);

        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class RedKoopaJumpOn : IKoopaState
    {
        private ISprite redKoopa;
        private RedKoopa enemyRedKoopa;
        public RedKoopaJumpOn(RedKoopa koopaRed)
        {
            enemyRedKoopa = koopaRed;
            redKoopa = SpriteFactory.Instance.CreateRedKoopaSprite("JumpOn");
        }
        public void Show()
        {
            enemyRedKoopa.State = new RedKoopaNormal(enemyRedKoopa);
        }

        public void JumpOn()
        {

        }

        public void Defeat()
        {
            enemyRedKoopa.State = new RedKoopaDefeated(enemyRedKoopa);
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

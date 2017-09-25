using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.KoopaStates
{
    public class RedKoopaJumpOnState : Interfaces.IKoopaState
    {
        private ISprite redKoopa;
        private KoopaObject.RedKoopa enemyRedKoopa;
        public RedKoopaJumpOnState(KoopaObject.RedKoopa koopaRed)
        {
            enemyRedKoopa = koopaRed;
            redKoopa = SpriteFactory.Instance.CreateJumpOnRedKoopaSprite();
        }
        public void transNormal()
        {
            enemyRedKoopa.redKoopaState = new RedKoopaNormalState(enemyRedKoopa);
        }

        public void transJumpOn()
        {

        }

        public void transDefeated()
        {
            enemyRedKoopa.redKoopaState = new RedKoopaDefeatedState(enemyRedKoopa);
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

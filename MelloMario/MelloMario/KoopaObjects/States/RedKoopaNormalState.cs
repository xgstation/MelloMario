using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.KoopaObjects.States
{
    public class RedKoopaNormalState : IKoopaState
    {
        private ISprite redKoopa;
        private KoopaObject.RedKoopa enemyRedKoopa;
        public RedKoopaNormalState(KoopaObject.RedKoopa koopaRed)
        {
            enemyRedKoopa = koopaRed;
            redKoopa = SpriteFactory.Instance.CreateRedKoopaSprite();
        }
        public void transNormal()
        {

        }

        public void transJumpOn()
        {
            enemyRedKoopa.redKoopaState = new RedKoopaJumpOnState(enemyRedKoopa);
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

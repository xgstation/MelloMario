using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.KoopaObjects.States
{
    public class RedKoopaDefeatedState : IKoopaState
    {

        private KoopaObject.RedKoopa enemyRedKoopa;
        private ISprite redKoopa;
        public RedKoopaDefeatedState(KoopaObject.RedKoopa koopaRed)
        {
            enemyRedKoopa = koopaRed;
            redKoopa = SpriteFactory.Instance.CreateDefeatedRedKoopaSprite();

        }
        public void transNormal()
        {
            enemyRedKoopa.redKoopaState = new RedKoopaNormalState(enemyRedKoopa);
        }

        public void transJumpOn()
        {
            enemyRedKoopa.redKoopaState = new RedKoopaJumpOnState(enemyRedKoopa);
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

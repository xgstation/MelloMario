using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.KoopaStates
{
    public class GreenKoopaNormalState : Interfaces.IKoopaState
    {
        private ISprite greenKoopa;
        private KoopaObject.GreenKoopa enemyGreenKoopa;
        public GreenKoopaNormalState(KoopaObject.GreenKoopa koopaGreen)
        {
            enemyGreenKoopa = koopaGreen;
            greenKoopa = SpriteFactory.Instance.CreateGreenKoopaSprite();
        }
        public void transNormal()
        {

        }

        public void transJumpOn()
        {
            enemyGreenKoopa.greenKoopaState = new GreenKoopaJumpOnState(enemyGreenKoopa);
        }

        public void transDefeated()
        {
            enemyGreenKoopa.greenKoopaState = new GreenKoopaDefeatedState(enemyGreenKoopa);
        }

        public void Update(GameTime gameTime)
        {
            greenKoopa.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            greenKoopa.Draw(spriteBatch, location);
        }
    }
}

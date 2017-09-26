using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.KoopaStates
{
    class GreenKoopaDefeated : IKoopaState
    {
        private GreenKoopa enemyGreenKoopa;
        private ISprite greenKoopa;

        public GreenKoopaDefeated(GreenKoopa koopaGreen)
        {
            enemyGreenKoopa = koopaGreen;
            greenKoopa = SpriteFactory.Instance.CreateGreenKoopaSprite("Defeated");
        }
        public void transNormal()
        {
            enemyGreenKoopa.greenKoopaState = new GreenKoopaNormal(enemyGreenKoopa);
        }

        public void transJumpOn()
        {
            enemyGreenKoopa.greenKoopaState = new GreenKoopaJumpOn(enemyGreenKoopa);
        }

        public void transDefeated()
        {

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

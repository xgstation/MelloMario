using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.KoopaObjects.States
{
    public class GreenKoopaJumpOnState : IKoopaState
    {
        private ISprite greenKoopa;
        private KoopaObject.GreenKoopa enemyGreenKoopa;
        public GreenKoopaJumpOnState(KoopaObject.GreenKoopa koopaGreen)
        {
            enemyGreenKoopa = koopaGreen;
            greenKoopa = SpriteFactory.Instance.CreateJumpOnGreenKoopaSprite();
        }
        public void transNormal()
        {
            enemyGreenKoopa.greenKoopaState = new GreenKoopaNormalState(enemyGreenKoopa);
        }

        public void transJumpOn()
        {

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

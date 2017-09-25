using MelloMario.KoopaObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.KoopaObjects.States
{
    public class GreenKoopaDefeatedState : IKoopaState
    {
        private GreenKoopa enemyGreenKoopa;
        private ISprite greenKoopa;

        public GreenKoopaDefeatedState(GreenKoopa koopaGreen)
        {
            enemyGreenKoopa = koopaGreen;
            greenKoopa = SpriteFactory.Instance.CreateDefeatedGreenKoopaSprite();
        }
        public void transNormal()
        {
            enemyGreenKoopa.greenKoopaState = new GreenKoopaNormalState(enemyGreenKoopa);
        }

        public void transJumpOn()
        {
            enemyGreenKoopa.greenKoopaState = new GreenKoopaJumpOnState(enemyGreenKoopa);
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

using MelloMario.GoombaObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.GoombaStates
{
    public class GoombaDefeatedState : IGoombaState
    {
        private ISprite goomba;
        private Goomba enemyGoomba;

        public GoombaDefeatedState(Goomba goomba1)
        {
            enemyGoomba = goomba1;
            goomba = SpriteFactory.Instance.CreateDefeatedGoombaSprite();

        }

        public void transNormal()
        {
            enemyGoomba.GoombaState = new GoombaNormalState(enemyGoomba);
        }
        public void transDefeated()
        {

        }
        public void Update(GameTime gameTime)
        {
            goomba.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            goomba.Draw(spriteBatch, location);
        }
    }
}

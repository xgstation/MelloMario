using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.GoombaStates
{
    class GoombaNormal: IGoombaState
    {
        private ISprite goomba;
        private Goomba enemyGoomba;

        public GoombaNormal(Goomba goomba1)
        {
            enemyGoomba = goomba1;
            goomba = SpriteFactory.Instance.CreateGoombaSprite("Normal");
        }

        public void Show()
        {
           
        }

        public void Defeat()
        {
            enemyGoomba.State = new GoombaDefeated(enemyGoomba);
        }

        public void Update(GameTime gameTime)
        {
            goomba.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            // TODO: calculate the location
            goomba.Draw(spriteBatch, location);
        }
    }
}

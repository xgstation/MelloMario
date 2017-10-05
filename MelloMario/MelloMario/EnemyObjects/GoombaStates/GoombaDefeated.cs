using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.EnemyObjects.GoombaStates
{
    class GoombaDefeated : IGoombaState
    {
        
        private Goomba enemyGoomba;

        public GoombaDefeated(Goomba goomba1)
        {
            enemyGoomba = goomba1;
           
        }

        public void Show()
        {
            enemyGoomba.State = new GoombaNormal(enemyGoomba);
        }
        public void Defeat()
        {
            
        }
        public void Update(GameTime time)
        {
         
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

        }
    }
}

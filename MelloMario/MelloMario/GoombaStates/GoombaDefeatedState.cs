using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario
{
    public class GoombaDefeatedState:IState
    {
        
        private Goomba enemyGoomba;

        public GoombaDefeatedState(Goomba goomba1)
        {
            enemyGoomba = goomba1;
           
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
         
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}

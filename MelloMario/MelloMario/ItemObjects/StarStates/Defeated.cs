using MelloMario.ItemObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.StarStates
{
    public class StarDefeatedState : IItemState
    {

        private Star starItem;
        public StarDefeatedState(Star star1)
        {
            starItem = star1;

        }
        public void transNormal()
        {
            starItem.State = new StarNormalState(starItem);
        }
        public void transDefeated()
        {

        }
        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

        }
    }
}

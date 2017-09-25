using MelloMario.Interfaces;
using MelloMario.ItemObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.StarState
{
    public class StarDefeatedState : ItemState
    {

        private Star starItem;
        public StarDefeatedState(Star star1)
        {
            starItem = star1;

        }
        public void transNormal()
        {
            starItem.starState = new StarNormalState(starItem);
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
